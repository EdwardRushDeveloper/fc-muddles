namespace Practice;

public class Fibonacci
{
    public static void Execute(string[] args)
    {

        //The Fibonacci sequence to 6 is fs=[0,1,1,2,3,5,8]. With zero-based indexing fs[5] = 5 (fs[5] is the index of the number that needs to be returned)

        int indexOfNumberToReturn = int.Parse(args[0]);
        Console.WriteLine($"Passed In Argument {indexOfNumberToReturn}");
        Console.Clear();
        Dictionary<int, long> memo = new Dictionary<int, long>();

        long returnedValue = Calculate(indexOfNumberToReturn, memo, "");
        Console.WriteLine(returnedValue);

        Winder(10);
    }

    protected static long Calculate(int n, Dictionary<int, long> memo, string side)
    {
        long output = 0;

        if (side == "first") Console.WriteLine($"Winding In: {n}");
        if (side == "second") Console.WriteLine($"Winding Out (2): {n}");

        if (memo.TryGetValue(n, out output))
        {
            Console.WriteLine($"Found: {output}");
            return output;
        }
        //base case
        if (n <= 1)
        {
            return n;
        }
        long first = Calculate(n - 1, memo, "first");
        long second = Calculate(n - 2, memo, "second");
        long f = first + second;
        Console.WriteLine($"Adding calculation {f}");
        memo.TryAdd(n, f);
        return f;
    }

    protected static int Winder(int number)
    {
        Console.WriteLine(number);
        if (number <= 1)
        {
            return number;
        }

        int one = Winder(number - 1);
        int two = Winder(number - 2);

        int result = one + two;

        Console.WriteLine($"Finish Up on 2 {result}");

        return result;

    }
}


/*

F(0)  = 0                          F(51)  = 20,365,011,074
F(1)  = 1                          F(52)  = 32,951,280,099
F(2)  = 1                          F(53)  = 53,316,291,173
F(3)  = 2                          F(54)  = 86,267,571,272
F(4)  = 3                          F(55)  = 139,583,862,445
F(5)  = 5                          F(56)  = 225,851,433,717
F(6)  = 8                          F(57)  = 365,435,296,162
F(7)  = 13                         F(58)  = 591,286,729,879
F(8)  = 21                         F(59)  = 956,722,026,041
F(9)  = 34                         F(60)  = 1,548,008,755,920
F(10) = 55                         F(61)  = 2,504,730,781,961
F(11) = 89                         F(62)  = 4,052,739,537,881
F(12) = 144                        F(63)  = 6,557,470,319,842
F(13) = 233                        F(64)  = 10,610,209,857,723
F(14) = 377                        F(65)  = 17,167,680,177,565
F(15) = 610                        F(66)  = 27,777,890,035,288
F(16) = 987                        F(67)  = 44,945,570,212,853
F(17) = 1,597                      F(68)  = 72,723,460,248,141
F(18) = 2,584                      F(69)  = 117,669,030,460,994
F(19) = 4,181                      F(70)  = 190,392,490,709,135
F(20) = 6,765                      F(71)  = 308,061,521,170,129
F(21) = 10,946                     F(72)  = 498,454,011,879,264
F(22) = 17,711                     F(73)  = 806,515,533,049,393
F(23) = 28,657                     F(74)  = 1,304,969,544,928,657
F(24) = 46,368                     F(75)  = 2,111,485,077,978,050
F(25) = 75,025                     F(76)  = 3,416,454,622,906,707
F(26) = 121,393                    F(77)  = 5,527,939,700,884,757
F(27) = 196,418                    F(78)  = 8,944,394,323,791,464
F(28) = 317,811                    F(79)  = 14,472,334,024,676,221
F(29) = 514,229                    F(80)  = 23,416,728,348,467,685
F(30) = 832,040                    F(81)  = 37,889,062,373,143,906
F(31) = 1,346,269                  F(82)  = 61,305,790,721,611,591
F(32) = 2,178,309                  F(83)  = 99,194,853,094,755,497
F(33) = 3,524,578                  F(84)  = 160,500,643,816,367,088
F(34) = 5,702,887                  F(85)  = 259,695,496,911,122,585
F(35) = 9,227,465                  F(86)  = 420,196,140,727,489,673
F(36) = 14,930,352                 F(87)  = 679,891,637,638,612,258
F(37) = 24,157,817                 F(88)  = 1,100,087,778,366,101,931
F(38) = 39,088,169                 F(89)  = 1,779,979,416,004,714,189
F(39) = 63,245,986                 F(90)  = 2,880,067,194,370,816,120
F(40) = 102,334,155                F(91)  = 4,660,046,610,375,530,309
F(41) = 165,580,141                F(92)  = 7,540,113,804,746,346,429
F(42) = 267,914,296                F(93)  = 12,200,160,415,121,876,738
F(43) = 433,494,437                F(94)  = 19,740,274,219,868,223,167
F(44) = 701,408,733                F(95)  = 31,940,434,634,990,099,905
F(45) = 1,134,903,170              F(96)  = 51,680,708,854,858,323,072
F(46) = 1,836,311,903              F(97)  = 83,621,143,489,848,422,977
F(47) = 2,971,215,073              F(98)  = 135,301,852,344,706,746,049
F(48) = 4,807,526,976              F(99)  = 218,922,995,834,555,169,026
F(49) = 7,778,742,049              F(100) = 354,224,848,179,261,915,075
F(50) = 12,586,269,025





*/