// ------------------------------------------------------------------------------------------------
//  <copyright file="Customer.cs" company="Business Management System Ltd.">
//      Copyright "2020" (c), Business Management System Ltd.
//      All rights reserved.
//  </copyright>
//  <author>Nikolay.Kostadinov</author>
// ------------------------------------------------------------------------------------------------

namespace LukoilExpedition.ConsoleClient.Models
{
    #region Using

    using System;

    #endregion

    public class Customer
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string TaxNumber { get; set; }
        public string Region { get; set; }
        public string Area { get; set; }
        public string PostalCode { get; set; }
        public bool IsValid { get; set; }
        public DateTime ModifiedOn { get; set; }
        public override string ToString() => $"Id: {this.Id} " + $"| Name: {this.Name}" + $"| City: {this.City}" + $"| Address: {this.Address}" +
                                             $"| TaxNumber: {this.TaxNumber}" + $"| Region: {this.Region}" + $"| Area: {this.Area}" + 
                                             $"| PostalCode: {this.PostalCode}" + $"| IsValid: {this.IsValid}" + $"| ModifiedOn: {this.ModifiedOn}";
    }
}