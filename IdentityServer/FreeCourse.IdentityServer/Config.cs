﻿using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace FreeCourse.IdentityServer;

public static class Config
{
    public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
    {
        new("resource_catalog")
        {
            Scopes = { "catalog_fullpermission" }
        },
        new("resource_photo_stock")
        {
            Scopes = { "photo_stock_fullpermission" }
        },
        new("resource_basket")
        {
            Scopes = { "basket_fullpermission" }
        },
        new("resource_discount")
        {
            Scopes = { "discount_fullpermission" }
        },
        new("resource_order")
        {
          Scopes  = { "order_fullpermission" }
        },
        new("resource_payment")
        {
            Scopes  = { "payment_fullpermission" }
        },
        new("resource_gateway")
        {
            Scopes  = { "gateway_fullpermission" }
        },        
        new(IdentityServerConstants.LocalApi.ScopeName)
    };

    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.Email(),
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource()
            {
                Name = "roles",
                DisplayName = "Roles",
                Description = "kullanıcı rolleri",
                UserClaims = new[] { "role" }
            }
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new("catalog_fullpermission", "Catalog api için ful erişim"),
            new("photo_stock_fullpermission", "Photo Stock api için ful erişim"),
            new("basket_fullpermission", "Basket api için ful erişim"),
            new("discount_fullpermission", "Discount api için ful erişim"),
            new("order_fullpermission", "Order api için ful erişim"),
            new("payment_fullpermission", "Payment api için ful erişim"),
            new("gateway_fullpermission", "Gateway api için ful erişim"),
            new(IdentityServerConstants.LocalApi.ScopeName)
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new()
            {
                ClientName = "Asp.Net Core Mvc",
                ClientId = "WebMvcClient",
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes =
                {
                    "catalog_fullpermission",
                    "photo_stock_fullpermission",
                    "gateway_fullpermission",
                    IdentityServerConstants.LocalApi.ScopeName
                }
            },
            new()
            {
                ClientName = "Asp.Net Core Mvc",
                ClientId = "WebMvcClientForUser",
                AllowOfflineAccess = true,
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                AllowedScopes =
                {
                    "basket_fullpermission",
                    "discount_fullpermission",
                    "order_fullpermission",
                    "payment_fullpermission",
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.OfflineAccess,
                    IdentityServerConstants.LocalApi.ScopeName,
                    "roles"
                },
                AccessTokenLifetime = 1*60*60,
                RefreshTokenExpiration = TokenExpiration.Absolute,
                AbsoluteRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60) - DateTime.Now).TotalSeconds,
                RefreshTokenUsage = TokenUsage.ReUse
            }
        };
}