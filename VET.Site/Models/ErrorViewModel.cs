// <copyright file="ErrorViewModel.cs" company="SysRC">
// Copyright (c) SysRC. All rights reserved.
// </copyright>

namespace VET.Site.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
