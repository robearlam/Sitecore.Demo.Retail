﻿//-----------------------------------------------------------------------
// <copyright file="CreditCardPaymentInputModelItem.cs" company="Sitecore Corporation">
//     Copyright (c) Sitecore Corporation 1999-2016
// </copyright>
// <summary>InputModel item parameter accepting credit card payment information.</summary>
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

using System.ComponentModel.DataAnnotations;

namespace Foundation.Commerce.Website.Models.InputModels
{
    public class CreditCardPaymentInputModelItem
    {
        [Required]
        public string CreditCardNumber { get; set; }

        [Required]
        public string PaymentMethodID { get; set; }

        [Required]
        public string ValidationCode { get; set; }

        [Required]
        public int ExpirationMonth { get; set; }

        [Required]
        public int ExpirationYear { get; set; }

        [Required]
        public string CustomerNameOnPayment { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public string PartyId { get; set; }
    }
}