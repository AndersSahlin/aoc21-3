
string[] input = File.ReadAllLines("input.txt");
string[] test = File.ReadAllLines("test.txt");
//input = test;
Console.WriteLine("Running diagnostics...");

uint powerConsumption = Diagnostics.GetPowerConsumption(input);
uint lifeSupportRating = Diagnostics.GetLifeSupportRating(input);

Console.WriteLine($"Diagnostics complete. Power consumption is: {powerConsumption}.");
Console.WriteLine($"Diagnostics complete. Life support rating is: {lifeSupportRating}.");
Console.ReadLine();

class Diagnostics
{
    public static uint GetPowerConsumption(string[] report)
    {
        uint gammaRate = 0;
        int digits = report[0].Length;

        for (int digit = 0; digit < digits; digit++)
        {
            gammaRate |= GetMostCommonBitInPosition(digit, report) << (digits - digit - 1);
        }

        uint epsilonRate = ~gammaRate & (uint)(Math.Pow(2, digits) - 1);
        return epsilonRate * gammaRate;
    }

    public static uint GetLifeSupportRating(string[] report)
    {
        int digits = report[0].Length;

        uint oxygenGeneratorRating = GetRating(report, digits, 0);
        uint co2ScrubberRating = GetRating(report, digits, 1);

        return oxygenGeneratorRating * co2ScrubberRating;
    }

    private static uint GetMostCommonBitInPosition(int digit, string[] items)
    {
        return (uint)(items.Average(item => item[digit] - '0') + 0.5f);
    }

    private static uint GetRating(string[] report, int digits, int rating)
    {
        for (int digit = 0; digit < digits; digit++)
        {
            uint bit = GetMostCommonBitInPosition(digit, report);
            report = report.Where(line => (line[digit] - '0') == (bit ^ rating)).ToArray();

            if (report.Length == 1) break;
        }

        return Convert.ToUInt32(report[0], 2);
    }
}