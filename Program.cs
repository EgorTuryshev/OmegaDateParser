using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

//unit();
main();

//[MethodImpl(MethodImplOptions.AggressiveInlining)]
static bool Year(string year)
{
    MonthDays md = MonthDays.Instance;
    bool r_val = uint.TryParse(year, out uint yearInt);
    if (r_val) md.Year = (int)yearInt;
    return r_val;
}
static bool NumericMonth(string month)
{
    MonthDays md = MonthDays.Instance;
    bool r_val = uint.TryParse(month, out uint monthInt) && monthInt > 0 && monthInt <= 12;
    if(r_val) md.Month = (int)monthInt;
    return r_val;
}
static bool RussianMonth(string month)
{
    MonthDays md = MonthDays.Instance;
    string[] RussianMonths =
    {
        "января",
        "февраля",
        "марта",
        "апреля",
        "мая",
        "июня",
        "июля",
        "августа",
        "сентября",
        "октября",
        "ноября",
        "декабря"
    };
    for(int i = 0; i < RussianMonths.Length; i++)
    {
        if (RussianMonths[i].Equals(month.ToLower()))
        {
            md.Month = i + 1;
            return true;
        }
    }
    return false;
}
static bool EnglishMonth(string month)
{
    MonthDays md = MonthDays.Instance;
    string[] EnglishMonths =
    {
        "january",
        "february",
        "march",
        "april",
        "may",
        "june",
        "july",
        "august",
        "september",
        "october",
        "november",
        "december"
    };
    for (int i = 0; i < EnglishMonths.Length; i++)
    {
        if (EnglishMonths[i].Equals(month.ToLower()))
        {
            md.Month = i + 1;
            return true;
        }
    }
    return false;
}
static bool EngMonth(string month)
{
    MonthDays md = MonthDays.Instance;
    string[] EngMonths =
    {
        "jan",
        "feb",
        "mar",
        "apr",
        "may",
        "jun",
        "jul",
        "aug",
        "sep",
        "oct",
        "nov",
        "dec"
    };
    for (int i = 0; i < EngMonths.Length; i++)
    {
        if (EngMonths[i].Equals(month.ToLower()))
        {
            md.Month = i + 1;
            return true;
        }
    }
    return false;
}
static bool Day(string day)
{
    MonthDays md = MonthDays.Instance;
    bool r_val = uint.TryParse(day, out uint dayInt) && dayInt > 0 && dayInt <= 31;
    if (r_val) md.Day = (int)dayInt;
    return r_val;
}

static async Task TaskManager(List<Task<bool>> RunningTasks)
{
    await Task.WhenAll(RunningTasks);
}

static void unit()
{
    List<string[]> splittersPresets = new List<string[]>
    {
        new string[] { ".", "." },
        new string[] { "/", "/" },
        new string[] { "-", "-" },
        new string[] { " ", " " },
        new string[] { " ", ", " },
        new string[] { ", ", " " }
    };

    List<DateTimeVerifier> tests = new List<DateTimeVerifier>
    {
        new DateTimeVerifier("14.09.2022"),
        new DateTimeVerifier("14/09/2022"),
        new DateTimeVerifier("14-09-2022"),
        new DateTimeVerifier("2022.09.14"),
        new DateTimeVerifier("2022/09/14"),
        new DateTimeVerifier("2022-09-14"),
        new DateTimeVerifier("14 сентября 2022"),
        new DateTimeVerifier("September 14, 2022"),
        new DateTimeVerifier("Sep 14, 2022"),
        new DateTimeVerifier("2022, September 14"),
        new DateTimeVerifier("2022, Sep 14"),
        new DateTimeVerifier("29/02/1900"),
        new DateTimeVerifier("28.02.2021")
    };

    foreach (DateTimeVerifier test in tests)
    {
        List<Task<bool>> answers = new()
        {
            Task.Run(() => test.Like(splittersPresets[0], Day, NumericMonth, Year)),
            Task.Run(() => test.Like(splittersPresets[1], Day, NumericMonth, Year)),
            Task.Run(() => test.Like(splittersPresets[2], Day, NumericMonth, Year)),
            Task.Run(() => test.Like(splittersPresets[0], Year, NumericMonth, Day)),
            Task.Run(() => test.Like(splittersPresets[1], Year, NumericMonth, Day)),
            Task.Run(() => test.Like(splittersPresets[2], Year, NumericMonth, Day)),
            Task.Run(() => test.Like(splittersPresets[3], Day, RussianMonth, Year)),
            Task.Run(() => test.Like(splittersPresets[4], EnglishMonth, Day, Year)),
            Task.Run(() => test.Like(splittersPresets[4], EngMonth, Day, Year)),
            Task.Run(() => test.Like(splittersPresets[5], Year, EnglishMonth, Day)),
            Task.Run(() => test.Like(splittersPresets[5], Year, EngMonth, Day))
        };

        TaskManager(answers).Wait();

        foreach (var (value, i) in answers.Select((value, i) => (value, i)))
        {
            if (value.Result) { Console.WriteLine(i); break; };
        }
    }
}

static void main()
{
    List<string[]> splittersPresets = new List<string[]>
    {
        new string[] { ".", "." },
        new string[] { "/", "/" },
        new string[] { "-", "-" },
        new string[] { " ", " " },
        new string[] { " ", ", " },
        new string[] { ", ", " " }
    };
    for(; ; )
    {
        DateTimeVerifier test = new(Console.ReadLine());
        List<Task<bool>> answers = new()
        {
            Task.Run(() => test.Like(splittersPresets[0], Day, NumericMonth, Year)),
            Task.Run(() => test.Like(splittersPresets[1], Day, NumericMonth, Year)),
            Task.Run(() => test.Like(splittersPresets[2], Day, NumericMonth, Year)),
            Task.Run(() => test.Like(splittersPresets[0], Year, NumericMonth, Day)),
            Task.Run(() => test.Like(splittersPresets[1], Year, NumericMonth, Day)),
            Task.Run(() => test.Like(splittersPresets[2], Year, NumericMonth, Day)),
            Task.Run(() => test.Like(splittersPresets[3], Day, RussianMonth, Year)),
            Task.Run(() => test.Like(splittersPresets[4], EnglishMonth, Day, Year)),
            Task.Run(() => test.Like(splittersPresets[4], EngMonth, Day, Year)),
            Task.Run(() => test.Like(splittersPresets[5], Year, EnglishMonth, Day)),
            Task.Run(() => test.Like(splittersPresets[5], Year, EngMonth, Day))
        };

        TaskManager(answers).Wait();
        int format = -1;
        foreach (var (value, i) in answers.Select((value, i) => (value, i)))
        {
            if (value.Result) { format = i; break; };
        }
        if (format != -1) Console.WriteLine($"Введенная дата соответствует формату {format + 1}");
        else Console.WriteLine("Введенная дата не соответствует ни одному формату");
    }
}

public class DateTimeVerifier
{
    private readonly string _dateString;
    public DateTimeVerifier(string dateString)
    {
        this._dateString = dateString;
    }
    public bool Like(string[] splitters, Func<string, bool> first,
        Func<string, bool> second, Func<string, bool> third)
    {
        string bufferDateString = this._dateString;
        try
        {
            MonthDays md = MonthDays.Instance;
            string firstString = bufferDateString.Substring(0, bufferDateString.IndexOf(splitters[0]));
            bufferDateString = bufferDateString.Remove(0, bufferDateString.IndexOf(splitters[0]) + splitters[0].Length);
            string secondString = bufferDateString.Substring(0, bufferDateString.IndexOf(splitters[1]));
            bufferDateString = bufferDateString.Remove(0, bufferDateString.IndexOf(splitters[1]) + splitters[1].Length);
            if (first(firstString) && second(secondString) && third(bufferDateString) && md.Day != -1 && md.Month != -1 && md.Year != -1)
            {
                bool isYearLeap = md.Year % 4 == 0 && md.Year % 100 != 0 || md.Year % 400 == 0;
                int[] days_in_months = { 31, isYearLeap ? 29 : 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
                if (md.Day <= days_in_months[md.Month - 1]) return true;
                else return false;
            }
            else
            {
                return false;
            }
        }
        catch (Exception) { return false; }
    }
}
public sealed class MonthDays
{
    private static ThreadLocal<MonthDays> instances = new ThreadLocal<MonthDays>(() => new MonthDays());
    private MonthDays() { }
    private int _day = -1;
    private int _month = -1;
    private int _year = -1;
    public int Day
    {
        get { return _day; }
        set { _day = value; }
    }
    public int Month
    {
        get { return _month; }
        set { _month = value; }
    }
    public int Year
    {
        get { return _year; }
        set { _year = value; }
    }
    public static MonthDays Instance
        => instances.Value;
}