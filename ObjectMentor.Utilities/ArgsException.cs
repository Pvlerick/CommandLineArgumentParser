using System;

namespace ObjectMentor.Utilities
{
    [Serializable]
    internal class ArgsException : Exception
    {
        public char ErrorArgumentId { get; set; }
        public string ErrorParameter { get; set; }
        public ErrorCode ErrorCode { get; set; }

        public ArgsException(string message) : base(message) { }

        public ArgsException(ErrorCode errorCode)
        {
            ErrorCode = errorCode;
        }

        public ArgsException(ErrorCode errorCode, string errorParameter)
        {
            ErrorCode = errorCode;
            ErrorParameter = errorParameter;
        }

        public ArgsException(ErrorCode errorCode, char errorArgumentId, string errorParameter)
        {
            ErrorCode = errorCode;
            ErrorArgumentId = errorArgumentId;
            ErrorParameter = errorParameter;
        }

        public string ErrorMessage
        {
            get
            {
                switch (ErrorCode)
                {
                    case ErrorCode.Ok:
                        return "TILT: Should not get here.";
                    case ErrorCode.UnexpectedArgument:
                        return $"Argument -{ErrorArgumentId} unexpected.";
                    case ErrorCode.MissingString:
                        return $"Could not find string parameter for -{ErrorArgumentId}.";
                    case ErrorCode.InvalidInteger:
                        return $"Argument -{ErrorArgumentId} expects an integer but was '{ErrorParameter}'.";
                    case ErrorCode.MissingInteger:
                        return $"Could not find integer parameter for ${ErrorArgumentId}.";
                    case ErrorCode.InvalidDouble:
                        return $"Argument ${ErrorArgumentId} expects a double but was '{ErrorParameter}'.";
                    case ErrorCode.MissingDouble:
                        return $"Could not find double parameter for ${ErrorArgumentId}.";
                    case ErrorCode.InvalidArgumentName:
                        return $"'{ErrorArgumentId}' is not a valid argument name.";
                    case ErrorCode.InvalidArgumentFormat:
                        return $"'{ErrorParameter}' is not a valid argument format.";
                    default:
                        return "";
                }
            }
        }
    }
}