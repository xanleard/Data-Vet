// <copyright file="InnerError.cs" company="SysRC">
// Copyright (c) SysRC. All rights reserved.
// </copyright>

namespace VET.Core
{
    using System.ComponentModel.DataAnnotations;

    public abstract class InnerError
    {
        public string Code { get; set; }

        [Required]
        public abstract string ErrorType { get; }
    }
}
