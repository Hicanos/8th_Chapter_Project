using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CostUtil
{
    private const long ConversionRate = 1000;
    private static readonly string[] Units = { "", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };

    public static string ConvertCurrency(double amount)
    {
        int unitIndex = 0;

        while (amount >= ConversionRate && unitIndex < Units.Length - 1)
        {
            amount /= ConversionRate;
            unitIndex++;
        }

        // 첫째 자리까지만 표기
        string formattedAmount = $"{amount:F1}";

        // 소수점 끝이 0이라면 안보이게
        formattedAmount = formattedAmount.TrimEnd('0').TrimEnd('.');

        // 값이 처리 후 비어 있다면, 그 값을 '0'으로 설정.
        if (string.IsNullOrEmpty(formattedAmount))
        {
            formattedAmount = "0";
        }

        return $"{formattedAmount}{Units[unitIndex]}";
    }
}
