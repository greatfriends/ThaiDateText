using GreatFriends.ThaiDateText;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GreatFriends.ThaiDateTextFacts {
  public class ThaiDateTextUtilFacts {

    // test helper
    private class Event {
      private List<DateTime> dates = new List<DateTime>();
      public void AddDate(DateTime d) {
        dates.Add(d);
      }
      public void SetDates(params DateTime[] dates) {
        foreach (var d in dates) AddDate(d);
      }
      public string DateText {
        get {
          return ThaiDateTextUtil.ThaiDateText(dates);
        }
      }
    }
    
    [Fact]
    public void DateTextForOneDate() {
      var ev = new Event();
      var d = new DateTime(2015, 5, 4); // Monday

      ev.AddDate(d);

      Assert.Equal("วันจันทร์ที่ 4 พฤษภาคม 2558", ev.DateText);
    }

    [Fact]
    public void DateTextForTwoDates() {
      var ev = new Event();
      var d1 = new DateTime(2015, 5, 4); // Monday
      var d2 = new DateTime(2015, 5, 5); // Tuesday

      ev.AddDate(d1);
      ev.AddDate(d2);

      Assert.Equal("วันจันทร์ถึงอังคารที่ 4-5 พฤษภาคม 2558", ev.DateText);
    }


    [Fact]
    public void DateTextForTwoSeperatedDates() {
      var ev = new Event();
      var d1 = new DateTime(2015, 5, 4); // Monday
      var d2 = new DateTime(2015, 5, 6); // Wednesday

      ev.AddDate(d1);
      ev.AddDate(d2);

      Assert.Equal("วันจันทร์และพุธที่ 4 และ 6 พฤษภาคม 2558", ev.DateText);
    }

    [Fact]
    public void DateTextForThreeSemiSeperatedDates() {
      var ev = new Event();
      var d1 = new DateTime(2015, 5, 4); // Monday
      var d2 = new DateTime(2015, 5, 6); // Wednesday
      var d3 = new DateTime(2015, 5, 7); // Thursday

      ev.SetDates(new DateTime[] { d1, d2, d3 });

      Assert.Equal("วันจันทร์และพุธ-พฤหัสบดีที่ 4 และ 6-7 พฤษภาคม 2558", ev.DateText);
    }

    [Fact]
    public void DateTextForTwoWeekCourse() {
      var ev = new Event();
      var d1 = new DateTime(2015, 5, 04); // Monday
      var d2 = new DateTime(2015, 5, 05); // Tuesday
      var d3 = new DateTime(2015, 5, 11); // Monday
      var d4 = new DateTime(2015, 5, 12); // Tuesday

      ev.SetDates(new DateTime[] { d1, d2, d3, d4 });

      Assert.Equal("วันจันทร์-อังคารที่ 4-5 และ 11-12 พฤษภาคม 2558", ev.DateText);
    }

    [Fact]
    public void DateTextForSingleDayMultipleWeeksCourse() {
      var ev = new Event();
      var d1 = new DateTime(2015, 5, 09); // Sat.
      var d2 = new DateTime(2015, 5, 16); // Sat.
      var d3 = new DateTime(2015, 5, 23); // Sat.
      var d4 = new DateTime(2015, 5, 30); // Sat.

      ev.SetDates(new DateTime[] { d1, d2, d3, d4 });

      Assert.Equal("วันเสาร์ที่ 9, 16, 23 และ 30 พฤษภาคม 2558", ev.DateText);
    }


    [Fact]
    public void DateTextForSingleDayMultipleWeeksAndMonthCourse() {
      var ev = new Event();
      var d1 = new DateTime(2015, 5, 16); // Sat.
      var d2 = new DateTime(2015, 5, 23); // Sat.
      var d3 = new DateTime(2015, 5, 30); // Sat.
      var d4 = new DateTime(2015, 6, 06); // Sat.

      ev.SetDates(new DateTime[] { d1, d2, d3, d4 });

      Assert.Equal("วันเสาร์ที่ 16, 23, 30 พฤษภาคม และวันเสาร์ที่ 6 มิถุนายน 2558", ev.DateText);
    }
    
  }
}
