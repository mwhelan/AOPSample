using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

//using System.Security.Permissions;
//using Seterlund.CodeGuard;

namespace AopSample.Core.Commands
{
    public class ValidatingException : Exception
    {
        public IEnumerable<ValidatingFailure> Errors { get; private set; }

        public ValidatingException(IEnumerable<ValidatingFailure> errors)
            : base(BuildErrorMesage(errors))
        {
            Errors = errors;
        }

        public ValidatingException(string propertyName, string error)
            : base(BuildErrorMesage(new List<ValidatingFailure>() { new ValidatingFailure(propertyName, error) }))
        {
            Errors = new List<ValidatingFailure>() { new ValidatingFailure(propertyName, error) };
        }

        public ValidatingException()
        {
        }

        public ValidatingException(string message)
            : base(message)
        {
        }

        public ValidatingException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected ValidatingException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }

        private static string BuildErrorMesage(IEnumerable<ValidatingFailure> errors)
        {
            var arr = errors.Select(x => "\r\n -- " + x.ErrorMessage).ToArray();
            return "Validation failed: " + string.Join("", arr);
        }

        //[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        //public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        //{
        //    Guard.That(() => info).IsNotNull();

        //    info.AddValue("Message", Message);

        //    base.GetObjectData(info, context);
        //}
    }
}