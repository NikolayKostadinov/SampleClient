// ------------------------------------------------------------------------------------------------
//  <copyright file="AuthenticateModel.cs" company="Business Management System Ltd.">
//      Copyright "2020" (c), Business Management System Ltd.
//      All rights reserved.
//  </copyright>
//  <author>Nikolay.Kostadinov</author>
// ------------------------------------------------------------------------------------------------

namespace LukoilExpedition.ConsoleClient.Models
{
    #region Using

    using System.ComponentModel.DataAnnotations;

    #endregion

    public class AuthenticateModel
    {
        [Required] public string UserName { get; set; }

        [Required] public string Password { get; set; }
    }
}