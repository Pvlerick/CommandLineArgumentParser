using System.Collections.Generic;
using System.Linq;

namespace ObjectMentor.Utilities
{
    public class Args
    {
        private IDictionary<char, IArgumentMarshaler> _marshalers;
        private ISet<char> _argsFound;
        private IIterator<string> _currentArgument;

        public Args(string schema, params string[] args)
        {
            _marshalers = new Dictionary<char, IArgumentMarshaler>();
            _argsFound = new HashSet<char>();

            ParseSchema(schema);
            ParseArgumentStrings(args.ToList());
        }

        private void ParseSchema(string schema)
        {
            foreach (var item in schema.Split(',')
                .Where(i => !string.IsNullOrWhiteSpace(i)))
            {
                ParseSchemaElement(item);
            }
        }

        private void ParseSchemaElement(string element)
        {
            var elementId = element[0];
            var elementTail = element.Substring(1);

            ValidateSchemaElementId(elementId);

            if (elementTail.Length == 0)
                _marshalers.Add(elementId, new BooleanArgumentMarshaler());
            else if (elementTail == "*")
                _marshalers.Add(elementId, new StringArgumentMarshaler());
            else if (elementTail == "#")
                _marshalers.Add(elementId, new IntegerArgumentMarshaler());
            else if (elementTail == "##")
                _marshalers.Add(elementId, new DoubleArgumentMarshaler());
            else if (elementTail == "[*]")
                _marshalers.Add(elementId, new StringArrayArgumentMarshaler());
            else
                throw new ArgsException(ErrorCode.InvalidArgumentFormat, elementId, elementTail);
        }

        private void ValidateSchemaElementId(char elementId)
        {
            if (!char.IsLetter(elementId))
                throw new ArgsException(ErrorCode.InvalidArgumentFormat, elementId, null);
        }

        private void ParseArgumentStrings(IList<string> argsList)
        {
            for (_currentArgument = argsList.GetIterator(); _currentArgument.HasNext;)
            {
                var argString = _currentArgument.Next();

                if (argString.StartsWith("-"))
                    ParseArgumentCharacters(argString.Substring(1));
                //else
                //{
                //    _currentArgument.Previous();
                //    break;
                //}

            }
        }

        private void ParseArgumentCharacters(string argChars)
        {
            foreach (var item in argChars)
                ParseArgumentCharacter(item);
        }

        private void ParseArgumentCharacter(char argChar)
        {
            IArgumentMarshaler m;

            if (!_marshalers.TryGetValue(argChar, out m))
                throw new ArgsException(ErrorCode.UnexpectedArgument, argChar, null);
            else
            {
                _argsFound.Add(argChar);
                try
                {
                    m.Set(_currentArgument);
                }
                catch (ArgsException e)
                {
                    e.ErrorArgumentId = argChar;
                    throw;
                }
            }
        }

        public bool Has(char arg) => _argsFound.Contains(arg);

        public int NextArgument() => _currentArgument.GetHashCode();

        public bool GetBoolean(char arg) => BooleanArgumentMarshaler.GetValue(_marshalers[arg]);

        public string GetString(char arg) => StringArgumentMarshaler.GetValue(_marshalers[arg]);

        public int GetInt32(char arg) => IntegerArgumentMarshaler.GetValue(_marshalers[arg]);

        public double GetDouble(char arg) => DoubleArgumentMarshaler.GetValue(_marshalers[arg]);

        public string[] GetStringArray(char arg) => StringArrayArgumentMarshaler.GetValue(_marshalers[arg]);
    }
}
