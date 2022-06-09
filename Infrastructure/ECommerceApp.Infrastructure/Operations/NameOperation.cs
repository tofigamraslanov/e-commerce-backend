﻿namespace ECommerceApp.Infrastructure.Operations;

public class NameOperation
{
    public static string CharacterRegulator(string name)
        => name
            .Replace("\"", "")
            .Replace("!", "")
            .Replace("^", "")
            .Replace("+", "")
            .Replace("%", "")
            .Replace("&", "")
            .Replace("/", "")
            .Replace("?", "")
            .Replace("(", "")
            .Replace(")", "")
            .Replace("=", "")
            .Replace("_", "")
            .Replace("-", "")
            .Replace("`", "")
            .Replace("~", "")
            .Replace("@", "")
            .Replace("#", "")
            .Replace("*", "")
            .Replace("|", "")
            .Replace(",", "")
            .Replace(".", "-")
            .Replace(";", "")
            .Replace(":", "")
            .Replace(">", "")
            .Replace("<", "")
            .Replace(":", "")
            .Replace("Ö", "o")
            .Replace("ö", "o")
            .Replace("Ü", "u")
            .Replace("ü", "u")
            .Replace("Ğ", "g")
            .Replace("ğ", "g")
            .Replace("I", "İ")
            .Replace("ı", "i")
            .Replace("Ç", "c")
            .Replace("ç", "c")
            .Replace("Ş", "s")
            .Replace("ş", "s");
}