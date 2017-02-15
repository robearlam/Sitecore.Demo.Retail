﻿//-----------------------------------------------------------------------
// <copyright file="BaseJsonResult.cs" company="Sitecore Corporation">
//     Copyright (c) Sitecore Corporation 1999-2016
// </copyright>
// <summary>Defines the BaseJsonResult class.</summary>
//-----------------------------------------------------------------------
// Copyright 2016 Sitecore Corporation A/S
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file 
// except in compliance with the License. You may obtain a copy of the License at
//       http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software distributed under the 
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, 
// either express or implied. See the License for the specific language governing permissions 
// and limitations under the License.
// -------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Sitecore.Commerce.Services;
using Sitecore.Foundation.Commerce.Managers;

namespace Sitecore.Reference.Storefront.Models.JsonResults
{
    public class BaseJsonResult : JsonResult
    {
        public BaseJsonResult()
        {
            Success = true;
        }

        public BaseJsonResult(ServiceProviderResult result)
        {
            Success = true;

            if (result != null)
            {
                SetErrors(result);
            }
        }

        public BaseJsonResult(string area, Exception exception)
        {
            Success = false;

            SetErrors(area, exception);
        }

        public BaseJsonResult(string url)
        {
            Success = false;

            Url = url;
        }

        public List<string> Errors { get; } = new List<string>();

        public bool HasErrors => Errors != null && Errors.Any();

        public bool Success { get; set; }

        public string Url { get; set; }

        public void SetErrors(ServiceProviderResult result)
        {
            Success = result.Success;
            if (result.SystemMessages.Count <= 0)
            {
                return;
            }

            var errors = result.SystemMessages;
            foreach (var error in errors)
            {
                var message = StorefrontManager.GetSystemMessage(error.Message, false);
                Errors.Add(string.IsNullOrEmpty(message) ? error.Message : message);
            }
        }

        public void SetErrors(string area, Exception exception)
        {
            Errors.Add($"{area}: {exception.Message}");
            Success = false;
        }

        public void SetErrors(List<string> errors)
        {
            if (!errors.Any())
            {
                return;
            }

            Success = false;
            Errors.AddRange(errors);
        }
    }
}