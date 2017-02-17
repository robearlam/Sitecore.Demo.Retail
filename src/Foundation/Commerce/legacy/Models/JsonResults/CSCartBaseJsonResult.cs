﻿//-----------------------------------------------------------------------
// <copyright file="CSCartBaseJsonResult.cs" company="Sitecore Corporation">
//     Copyright (c) Sitecore Corporation 1999-2016
// </copyright>
// <summary>CS Reference Storefront specific version/overrides of the common CartBaseJsonResult.</summary>
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

using System.Linq;
using Sitecore.Commerce.Connect.CommerceServer.Orders.Models;
using Sitecore.Commerce.Entities.Carts;
using Sitecore.Commerce.Services;
using Sitecore.Foundation.Commerce.Managers;
using Sitecore.Foundation.Commerce.Extensions;

namespace Sitecore.Reference.Storefront.Models.JsonResults
{
    public class CSCartBaseJsonResult : CartBaseJsonResult
    {
        public CSCartBaseJsonResult()
        {
        }

        public CSCartBaseJsonResult(ServiceProviderResult result)
            : base(result)
        {
        }

        public override void Initialize(Cart cart)
        {
            base.Initialize(cart);

            var commerceCart = cart as CommerceCart;
            if (commerceCart == null)
            {
                return;
            }

            if (commerceCart.OrderForms.Count > 0)
            {
                foreach (var promoCode in commerceCart.OrderForms[0].PromoCodes ?? Enumerable.Empty<string>())
                {
                    PromoCodes.Add(promoCode);
                }
            }

            var totalSavings = cart.Lines.Sum(lineitem => ((CommerceTotal) lineitem.Total).LineItemDiscountAmount);
            totalSavings += ((CommerceTotal) cart.Total).OrderLevelDiscountAmount;
            Discount = totalSavings.ToCurrency(StorefrontManager.GetCustomerCurrency());
        }
    }
}