using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GreatFriends.ThaiDateText {

  public class DateRange {
    // private EventDate d;


    public DateTime Date { get; set; }
    public DateTime ToDate { get; set; }

    public DateRange(DateTime d) {
      Date = d;
      ToDate = d;
    }

    public static List<DateRange> Simplify(IEnumerable<DateTime> dates) {
      var col = new List<DateRange>();
      foreach (var d in dates.OrderBy(x => x.Date)) {
        col.Add(new DateRange(d.Date));
      }
      return Simplify(col);
    }

    public static List<DateRange> Simplify(List<DateRange> col) {
      DateRange previousDay = new DateRange(DateTime.MinValue);
      var newCol = new List<DateRange>();

      foreach (var thisDay in col) {
        if (thisDay.Date != previousDay.Date.AddDays(1)) {
          newCol.Add(thisDay);
        }
        else {
          previousDay.ToDate = thisDay.Date;
        }
        previousDay = thisDay;
      }

      return newCol;
    }
  }
}
