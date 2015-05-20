using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreatFriends.ThaiDateText {
  public static class ThaiDateTextUtil {

    public static string ThaiDateText(this DateTime date) {
      return ThaiDateText(new DateTime[] { date });
    }
    public static string ThaiDateText(this IEnumerable<DateTime> dates) {
      var days = dates.ToArray();
      var d1 = days.Min();
      var d2 = days.Max();

      if (dates.Count() == 0) {
        return "";
      }
      else if (dates.Count() == 1) {
        return string.Format("วัน{0}ที่ {1}", ThaiDayOfWeek(d1.Date), ToThaiDate(d1.Date));
      }
      else if (isContinuousDates(days)) {
        return string.Format("วัน{0}ถึง{1}ที่ {2}-{3} {4} {5}",
          ThaiDayOfWeek(d1.Date), ThaiDayOfWeek(d2.Date),
          d1.Date.Day, d2.Date.Day,
          thaiMonths[d1.Date.Month - 1], d1.Date.Year + 543);
      }
      else {

        var dateRanges = DateRange.Simplify(days.Select(x => x.Date));

        DateRange firstDate = dateRanges.First();
        var distinctMonths = dateRanges.Select(i => i.Date.Month).Distinct().ToList();
        var year = firstDate.Date.Year + 543;
        var text = "";

        for (int i = 0; i < distinctMonths.Count(); i++) {
          int m = distinctMonths[i];
          bool hasMore = i + 1 < distinctMonths.Count();
          var datesInMonth = dateRanges.Where(dr => dr.Date.Month == m).ToList();

          if (text != "") text += " และ";
          text += friendlyDateTextForMonth(datesInMonth, hasMore);
        }

        return text + " " + year;
      }
    }
  
    private static string friendlyDateTextForMonth(List<DateRange> dateRanges, bool hasMore) {
      var days = "";
      var months = "";
      var dates = "";

      for (int i = 0; i < dateRanges.Count(); i++) {
        DateRange d = dateRanges[i];

        string s;
        if (d.Date == d.ToDate) {
          s = ThaiDayOfWeek(d.Date);
        }
        else {
          s = ThaiDayOfWeek(d.Date) + "-" + ThaiDayOfWeek(d.ToDate);
        }
        if (s != days) {
          if (days != "") { days = days + "และ"; }
          days += s;
        }

        //
        s = "";
        if (d.Date == d.ToDate) {
          s = d.Date.Day.ToString();
        }
        else {
          s = d.Date.Day + "-" + d.ToDate.Day;
        }
        if (s != dates) {
          if (dates != "") {
            if (i + 1 < dateRanges.Count() || hasMore) {
              dates += ", ";
            }
            else {
              dates += " และ ";
            }
          }
          dates += s;
        }

        //
        var m = thaiMonths[d.Date.Month - 1];
        if (m != months) {
          if (months != "") months += " และ ";
          months += m;
        }
      }

      return "วัน" + days + "ที่ " + dates + " " + months;
    }

    private static bool isContinuousDates(DateTime[] Dates) {
      var d1 = Dates.Min(ed => ed.Date.Date);
      var d2 = Dates.Max(ed => ed.Date.Date);
      var days = ((d2 - d1).Days + 1);

      return Dates.Count() == days;
    }


    #region utils

    private static readonly string[] thaiMonths = { "มกราคม", "กุมภาพันธ์", "มีนาคม", "เมษายน", "พฤษภาคม", "มิถุนายน", "กรกฎาคม", "สิงหาคม", "กันยายน", "ตุลาคม", "พฤศจิกายน", "ธันวาคม" };
    private static string ToThaiDate(DateTime? d) {
      if (d.HasValue) {
        return ToThaiDate(d.Value);
      }
      return string.Empty;
    }
    private static string ToThaiDate(DateTime d) {
      return string.Format("{0} {1} {2}", d.Day, thaiMonths[d.Month - 1], d.Year + 543);
    }
    private static string ToThaiDate(DateTime d, bool showTime) {
      return string.Format("{0} {1} {2} {3:HH:mm}", d.Day, thaiMonths[d.Month - 1], d.Year + 543, d);
    }
    private static readonly string[] thaiDOW = { "อาทิตย์", "จันทร์", "อังคาร", "พุธ", "พฤหัสบดี", "ศุกร์", "เสาร์" };
    private static string ThaiDayOfWeek(DateTime? d) {
      if (d.HasValue) {
        return ThaiDayOfWeek(d.Value);
      }
      return string.Empty;
    }
    private static string ThaiDayOfWeek(DateTime d) {
      return thaiDOW[Convert.ToInt32(d.DayOfWeek)];
    }
    #endregion
  }
}
