// <copyright file="Error.cs" company="SysRC">
// Copyright (c) SysRC. All rights reserved.
// </copyright>

namespace VET.Core
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Error
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public string Message { get; set; }

        public string Target { get; set; }

        public IEnumerable<Error> Details { get; set; }

        public InnerError InnerError { get; set; }
    }
}
