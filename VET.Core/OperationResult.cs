// <copyright file="OperationResult.cs" company="SysRC">
// Copyright (c) SysRC. All rights reserved.
// </copyright>

namespace VET.Core
{
    using System.Collections.Generic;
    using System.Linq;

    public class OperationResult
    {
        public OperationResult()
        {
            this.ValidationMessages = new Dictionary<string, IEnumerable<string>>();
            this.Succeeded = false;
        }

        public OperationResult(bool succeeded)
            : this()
        {
            this.Succeeded = succeeded;
        }

        public OperationResult(Error error)
            : this()
        {
            this.Error = error;
        }

        public OperationResult(IEnumerable<string> errors)
        : this()
        {
            this.ValidationMessages = new Dictionary<string, IEnumerable<string>>
            {
                [string.Empty] = errors,
            };
        }

        public OperationResult(IDictionary<string, IEnumerable<string>> validationMessages)
            : this()
        {
            this.ValidationMessages = new Dictionary<string, IEnumerable<string>>(validationMessages);
        }

        public IEnumerable<string> Errors
        {
            get
            {
                return this.ValidationMessages.SelectMany(s => s.Value);
            }
        }

        public IReadOnlyDictionary<string, IEnumerable<string>> ValidationMessages { get; protected set; }

        public bool Succeeded { get; protected set; }

        public Error Error { get; set; }

        public static implicit operator bool(OperationResult value)
        {
            return value.Succeeded;
        }

        public static implicit operator OperationResult(bool value)
        {
            return new OperationResult(value);
        }
    }
}
