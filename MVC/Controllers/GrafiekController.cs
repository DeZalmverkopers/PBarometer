using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC.Models;
using Domain.Dashboards;

//Seppe

namespace MVC.Controllers
{
  public partial class GrafiekController : Controller
  {

    //Grafiek grafiek = new Grafiek()
    //{

    //}

    // GET: Grafiek
    public virtual ActionResult Index()
    {
      return View();
    }

    public virtual JsonResult BarChartData()
    {
      Chart chart = new Chart
      {
        labels = new string[] { "Januari", "Februari", "Maart", "April", "Mei", "Juni", "Juli", "Augustus", "September", "Oktober", "November", "December" },
        datasets = new List<Datasets>()
      };
      List<Datasets> dataSet = new List<Datasets>
      {
        new Datasets()
        {
          data = new int[] { 28, 48, 40, 19, 86, 27, 90, 20, 45, 65, 34, 22 },
          backgroundColor = new string[] { "#FF0000", "#800000", "#808000", "#008080", "#800080", "#0000FF", "#000080", "#999999", "#E9967A", "#CD5C5C", "#1A5276", "#27AE60" },
          borderColor = new string[] { "#FF0000", "#800000", "#808000", "#008080", "#800080", "#0000FF", "#000080", "#999999", "#E9967A", "#CD5C5C", "#1A5276", "#27AE60" },
          borderWidth = "1"
        }
      };
      chart.datasets = dataSet;
      return Json(chart, JsonRequestBehavior.AllowGet);
    }

    public virtual JsonResult DonutPieChartData()
    {
      Chart chart = new Chart
      {
        labels = new string[] { "Francken", "De Wever", "Di Rupo", "Calvo", "Michel" },
        datasets = new List<Datasets>()
      };
      List<Datasets> dataSet = new List<Datasets>
      {
        new Datasets()
        {
          data = new int[] { 28, 48, 40, 19, 86 },
          backgroundColor = new string[] { "#FF0000", "#800000", "#808000", "#008080", "#800080", "#0000FF", "#000080", "#999999", "#E9967A", "#CD5C5C", "#1A5276", "#27AE60" },
          borderColor = new string[] { "#FF0000", "#800000", "#808000", "#008080", "#800080", "#0000FF", "#000080", "#999999", "#E9967A", "#CD5C5C", "#1A5276", "#27AE60" },
          borderWidth = "1"
        }
      };
      chart.datasets = dataSet;
      return Json(chart, JsonRequestBehavior.AllowGet);
    }

    public virtual JsonResult LineChartData()
    {
      Chart chart = new Chart
      {
        labels = new string[] { "Januari", "Februari", "Maart", "April", "Mei", "Juni", "Juli", "Augustus", "September", "Oktober", "November", "December" },
        datasets = new List<Datasets>()
      };
      List<Datasets> dataSet = new List<Datasets>
      {
        new Datasets()
        {
          data = new int[] { 28, 48, 40, 19, 86, 27, 90, 20, 45, 65, 34, 22 },
          backgroundColor = null,
          borderColor = new string[] { "#800080" },
          borderWidth = "1"
        }
      };
      chart.datasets = dataSet;
      return Json(chart, JsonRequestBehavior.AllowGet);
    }

    public virtual JsonResult MultiBarChartData()
    {

      Chart chart = new Chart
      {
        labels = new string[] { "Januari", "Februari", "Maart", "April", "Mei", "Juni", "Juli", "Augustus", "September", "Oktober", "November", "December" },
        datasets = new List<Datasets>()
      };
      List<Datasets> dataSet = new List<Datasets>
      {
        new Datasets()
        {
          label = "Huidig jaar",
          data = new int[] { 28, 48, 40, 19, 86, 27, 90, 20, 45, 65, 34, 22 },
          //backgroundColor = new string[] { "#FF0000", "#800000", "#808000", "#008080", "#800080", "#0000FF", "#000080", "#999999", "#E9967A", "#CD5C5C", "#1A5276", "#27AE60" },
          //borderColor = new string[] { "#FF0000", "#800000", "#808000", "#008080", "#800080", "#0000FF", "#000080", "#999999", "#E9967A", "#CD5C5C", "#1A5276", "#27AE60" },

          backgroundColor = new string[] { "#FF0000", "#FF0000", "#FF0000", "#FF0000", "#FF0000", "#FF0000", "#FF0000", "#FF0000", "#FF0000", "#FF0000", "#FF0000", "#FF0000" },
          borderColor = new string[] { "#FF0000", "#FF0000", "#FF0000", "#FF0000", "#FF0000", "#FF0000", "#FF0000", "#FF0000", "#FF0000", "#FF0000", "#FF0000", "#FF0000" },

          borderWidth = "1"
        },
        new Datasets()
        {
          label = "Vorig jaar",
          data = new int[] { 65, 59, 80, 81, 56, 55, 40, 55, 66, 77, 88, 34 },
          //backgroundColor = new string[] { "#FF0000", "#800000", "#808000", "#008080", "#800080", "#0000FF", "#000080", "#999999", "#E9967A", "#CD5C5C", "#1A5276", "#27AE60" },
          //borderColor = new string[] { "#FF0000", "#800000", "#808000", "#008080", "#800080", "#0000FF", "#000080", "#999999", "#E9967A", "#CD5C5C", "#1A5276", "#27AE60" },

          backgroundColor = new string[] { "#808000", "#808000", "#808000", "#808000", "#808000", "#808000", "#808000", "#808000", "#808000", "#808000", "#808000", "#808000" },
          borderColor = new string[] { "#808000", "#808000", "#808000", "#808000", "#808000", "#808000", "#808000", "#808000", "#808000", "#808000", "#808000", "#808000" },

          borderWidth = "1"
        }
      };
      chart.datasets = dataSet;
      return Json(chart, JsonRequestBehavior.AllowGet);
    }


    public virtual JsonResult MultiDonutChartData()
    {
      Chart chart = new Chart
      {
        labels = new string[] { "Francken", "De Wever", "Di Rupo", "Calvo", "Michel" },
        datasets = new List<Datasets>()
      };
      List<Datasets> dataSet = new List<Datasets>
      {
        new Datasets()
        {
          label = "Huidig jaar",
          data = new int[] { 28, 48, 40, 19, 86 },
          //backgroundColor = new string[] { "#FF0000", "#800000", "#808000", "#008080", "#800080", "#0000FF", "#000080", "#999999", "#E9967A", "#CD5C5C", "#1A5276", "#27AE60" },
          backgroundColor = new string[] { "#FF0000", "#800000", "#808000", "#008080", "#800080" },
          //borderColor = new string[] { "#FF0000", "#800000", "#808000", "#008080", "#800080", "#0000FF", "#000080", "#999999", "#E9967A", "#CD5C5C", "#1A5276", "#27AE60" },
          borderColor = new string[] { "#000000", "#000000", "#000000", "#000000", "#000000" },
          borderWidth = "1"
        },
        new Datasets()
        {
          label = "Vorig jaar",
          data = new int[] { 30, 35, 48, 22, 66 },
          //backgroundColor = new string[] { "#FF0000", "#800000", "#808000", "#008080", "#800080", "#0000FF", "#000080", "#999999", "#E9967A", "#CD5C5C", "#1A5276", "#27AE60" },
          backgroundColor = new string[] { "#FF0000", "#800000", "#808000", "#008080", "#800080" },
          //borderColor = new string[] { "#FF0000", "#800000", "#808000", "#008080", "#800080", "#0000FF", "#000080", "#999999", "#E9967A", "#CD5C5C", "#1A5276", "#27AE60" },
          borderColor = new string[] { "#000000", "#000000", "#000000", "#000000", "#000000" },
          borderWidth = "1"
        }
      };
      chart.datasets = dataSet;
      return Json(chart, JsonRequestBehavior.AllowGet);
    }


    public virtual JsonResult MultiLineChartData()
    {
      Chart chart = new Chart
      {
        labels = new string[] { "Januari", "Februari", "Maart", "April", "Mei", "Juni", "Juli", "Augustus", "September", "Oktober", "November", "December" },
        datasets = new List<Datasets>()
      };
      List<Datasets> dataSet = new List<Datasets>
      {
        new Datasets()
        {
          label = "Huidig jaar",
          data = new int[] { 28, 48, 40, 19, 86, 27, 90, 20, 45, 65, 34, 22 },
          borderColor = new string[] { "#000000" },
          backgroundColor = new string[] { "#FF0000" },
          borderWidth = "1"
        },
        new Datasets()
        {
          label = "Vorig jaar",
          data = new int[] { 65, 59, 80, 81, 56, 55, 40, 55, 66, 77, 88, 34 },
          borderColor = new string[] { "#000000" },
          backgroundColor = new string[] { "#808000" },
          borderWidth = "1"
        }
      };
      chart.datasets = dataSet;
      return Json(chart, JsonRequestBehavior.AllowGet);
    }


    //public JsonResult BarChartKruising()
    //{

    //}

    //public JsonResult PieChartKruising()
    //{

    //}

    public virtual JsonResult BarChartVergelijking()
    {
      //Chart chart = new Chart
      //{
      //  labels = new string[] { "Item 1", "Item 2", "Item 3", "Item 4", "Item 5" },
      //  datasets = new List<Datasets>()
      //};
      //List<Datasets> dataSet = new List<Datasets>
      //{
      //  new Datasets()
      //  {
      //    data = new int[] { 28, 48, 40, 19, 86 },
      //    backgroundColor = new string[] { "#FF0000", "#800000", "#808000", "#008080", "#800080" },
      //    borderColor = new string[] { "#FF0000", "#800000", "#808000", "#008080", "#800080" },
      //    borderWidth = "1"
      //  }
      //};
      //chart.datasets = dataSet;
      //return Json(chart, JsonRequestBehavior.AllowGet);

      return MaakGrafiek1Dataset(new string[] { "Item 1", "Item 2", "Item 3", "Item 4", "Item 5" }, new int[] { 28, 48, 40, 19, 86 }, new string[] { "#FF0000", "#800000", "#808000", "#008080", "#800080" }, new string[] { "#FF0000", "#800000", "#808000", "#008080", "#800080" });
    }


    public virtual JsonResult LineChartVergelijking()
    {
      Chart chart = new Chart
      {
        labels = new string[] { "Maandag", "Dinsdag", "Woensdag", "Donderdag", "Vrijdag", "Zaterdag", "Zondag" },
        //chart.labels = new string[] {  };
        datasets = new List<Datasets>()
      };
      List<Datasets> dataSet = new List<Datasets>
      {
        new Datasets()
        {
          label = "Item 1",
          data = new int[] { 28, 48, 40, 19, 86, 27, 90 },

          //borderColor = new string[] { "#000000" },
          //backgroundColor = new string[] { "#FF0000" },
          borderColor = new string[] { "#FF0000" },
          //backgroundColor = new string[] { "#000000" },

          borderWidth = "4"
        },
        new Datasets()
        {
          label = "Item 2",
          data = new int[] { 65, 59, 80, 81, 56, 55, 40 },

          //borderColor = new string[] { "#000000" },
          //backgroundColor = new string[] { "#800000" },
          borderColor = new string[] { "#800000" },
          //backgroundColor = new string[] { "#000000" },

          borderWidth = "4"
        },
        new Datasets()
        {
          label = "Item 3",
          data = new int[] { 34, 88, 77, 66, 55, 40, 15 },

          //borderColor = new string[] { "#000000" },
          //backgroundColor = new string[] { "#808000" },
          borderColor = new string[] { "#808000" },
          //backgroundColor = new string[] { "#000000" },

          borderWidth = "4"
        },
        new Datasets()
        {
          label = "Item 4",
          data = new int[] { 20, 45, 65, 34, 22, 95, 33 },

          //borderColor = new string[] { "#000000" },
          //backgroundColor = new string[] { "#008080" },
          borderColor = new string[] { "#008080" },
          //backgroundColor = new string[] { "#000000" },

          borderWidth = "4"
        },
        new Datasets()
        {
          label = "Item 5",
          data = new int[] { 55, 66, 77, 88, 34, 99, 44 },

          //borderColor = new string[] { "#000000" },
          //backgroundColor = new string[] { "#800080" },
          borderColor = new string[] { "#800080" },
          //backgroundColor = new string[] { "#000000" },

          borderWidth = "4"
        }
      };
      chart.datasets = dataSet;
      return Json(chart, JsonRequestBehavior.AllowGet);
      //return MaakGrafiekMeerdereDatasets(5,
      //  new string[] { "Maandag", "Dinsdag", "Woensdag", "Donderdag", "Vrijdag", "Zaterdag", "Zondag" },
      //  new string[] { "Item1", "Item2", "Item3", "Item4", "Item5" },
      //  new int[5, 7] { { 28, 48, 40, 19, 86, 27, 90 }, { 65, 59, 80, 81, 56, 55, 40 }, { 34, 88, 77, 66, 55, 40, 15 }, { 20, 45, 65, 34, 22, 95, 33 }, { 55, 66, 77, 88, 34, 99, 44 } },
      //  new string[5, 1] { { "#FF0000" }, { "#800000" }, { "#808000" }, { "#008080" }, { "#800080" } },
      //  new string[5, 1] { { "#FF0000" }, { "#800000" }, { "#808000" }, { "#008080" }, { "#800080" } });
    }


    public virtual JsonResult LineChartAantalTweets()
    {
      //Chart chart = new Chart
      //{
      //  labels = new string[] { "Maandag", "Dinsdag", "Woensdag", "Donderdag", "Vrijdag", "Zaterdag", "Zondag" }
      //  //labels = new string[] {  },
      //  datasets = new List<Datasets>()
      //};
      //List<Datasets> dataSet = new List<Datasets>
      //{
      //  new Datasets()
      //  {
      //    data = new int[] { 28, 48, 40, 19, 86, 27, 90 },
      //    backgroundColor = null,
      //    borderColor = new string[] { "#800080" },
      //    borderWidth = "1"
      //  }
      //};
      //chart.datasets = dataSet;
      //return Json(chart, JsonRequestBehavior.AllowGet);

      return MaakGrafiek1Dataset(new string[] { "Maandag", "Dinsdag", "Woensdag", "Donderdag", "Vrijdag", "Zaterdag", "Zondag" }, new int[] { 28, 48, 40, 19, 86, 27, 90 }, null, new string[] { "#800080" });
    }

    public virtual JsonResult MaakGrafiek1Dataset(string[] labels, int[] data, string[] backgroundcolor, string[] bordercolor)
    {
      Chart chart = new Chart
      {
        labels = labels,
        datasets = new List<Datasets>()
      };
      List<Datasets> dataSet = new List<Datasets>
      {
        new Datasets()
        {
          data = data,
          backgroundColor = backgroundcolor,
          borderColor = bordercolor,
          borderWidth = "1"
        }
      };
      chart.datasets = dataSet;
      return Json(chart, JsonRequestBehavior.AllowGet);

    }


    //public virtual JsonResult MaakGrafiekMeerdereDatasets(int aantalDatasets, string[] alleLabels, string[] label, int[,] data, string[,] backgroundcolor, string[,] bordercolor)
    //{
    //  string labelTemp1 = label[0];
    //  string labelTemp2 = label[1];
    //  string labelTemp3 = label[2];
    //  string labelTemp4 = label[3];
    //  string labelTemp5 = label[4];

    //  int[] dataTemp1 = null;
    //  int[] dataTemp2 = null;
    //  int[] dataTemp3 = null;
    //  int[] dataTemp4 = null;
    //  int[] dataTemp5 = null;

    //  string[] backgroundcolorTemp1 = null;
    //  string[] backgroundcolorTemp2 = null;
    //  string[] backgroundcolorTemp3 = null;
    //  string[] backgroundcolorTemp4 = null;
    //  string[] backgroundcolorTemp5 = null;


    //  string[] bordercolorTemp1 = null;
    //  string[] bordercolorTemp2 = null;
    //  string[] bordercolorTemp3 = null;
    //  string[] bordercolorTemp4 = null;
    //  string[] bordercolorTemp5 = null;


    //  for (int i = 0; i <= data.GetUpperBound(0); i++)
    //  {
    //    //dataTemp1 = new int[] { data[0, i] };
    //    //dataTemp2 = new int[] { data[1, i] };
    //    //dataTemp3 = new int[] { data[2, i] };
    //    //dataTemp4 = new int[] { data[3, i] };
    //    //dataTemp5 = new int[] { data[4, i] };

    //    dataTemp1 = new int[] { data[i, 0] };
    //    dataTemp2 = new int[] { data[i, 1] };
    //    dataTemp3 = new int[] { data[i, 2] };
    //    dataTemp4 = new int[] { data[i, 3] };
    //    dataTemp5 = new int[] { data[i, 4] };
    //  }

    //  for (int i = 0; i <= backgroundcolor.GetUpperBound(0); i++)
    //  {
    //    //backgroundcolorTemp1 = new string[] { backgroundcolor[0, i] };
    //    //backgroundcolorTemp2 = new string[] { backgroundcolor[1, i] };
    //    //backgroundcolorTemp3 = new string[] { backgroundcolor[2, i] };
    //    //backgroundcolorTemp4 = new string[] { backgroundcolor[3, i] };
    //    //backgroundcolorTemp5 = new string[] { backgroundcolor[4, i] };

    //    backgroundcolorTemp1 = new string[] { backgroundcolor[i, 0] };
    //    backgroundcolorTemp2 = new string[] { backgroundcolor[i, 1] };
    //    backgroundcolorTemp3 = new string[] { backgroundcolor[i, 2] };
    //    backgroundcolorTemp4 = new string[] { backgroundcolor[i, 3] };
    //    backgroundcolorTemp5 = new string[] { backgroundcolor[i, 4] };
    //  }

    //  for (int i = 0; i <= bordercolor.GetUpperBound(0); i++)
    //  {
    //    //bordercolorTemp1 = new string[] { bordercolor[0, i] };
    //    //bordercolorTemp2 = new string[] { bordercolor[1, i] };
    //    //bordercolorTemp3 = new string[] { bordercolor[2, i] };
    //    //bordercolorTemp4 = new string[] { bordercolor[3, i] };
    //    //bordercolorTemp5 = new string[] { bordercolor[4, i] };

    //    bordercolorTemp1 = new string[] { bordercolor[i, 0] };
    //    bordercolorTemp2 = new string[] { bordercolor[i, 1] };
    //    bordercolorTemp3 = new string[] { bordercolor[i, 2] };
    //    bordercolorTemp4 = new string[] { bordercolor[i, 3] };
    //    bordercolorTemp5 = new string[] { bordercolor[i, 4] };
    //  }


    //  List<Datasets> dataSet = new List<Datasets>();

    //  Chart chart = new Chart
    //  {
    //    labels = alleLabels,
    //    datasets = new List<Datasets>()
    //  };

    //  switch (aantalDatasets)
    //  {
    //    case 2:

    //      dataSet.Add(new Datasets()
    //      {
    //        label = labelTemp1,
    //        data = dataTemp1,
    //        backgroundColor = backgroundcolorTemp1,
    //        borderColor = bordercolorTemp1,
    //        borderWidth = "4"
    //      });
    //      dataSet.Add(new Datasets()
    //      {
    //        label = labelTemp2,
    //        data = dataTemp2,
    //        backgroundColor = backgroundcolorTemp2,
    //        borderColor = bordercolorTemp2,
    //        borderWidth = "4"
    //      });
    //      break;

    //    case 3:

    //      dataSet.Add(new Datasets()
    //      {
    //        label = labelTemp1,
    //        data = dataTemp1,
    //        backgroundColor = backgroundcolorTemp1,
    //        borderColor = bordercolorTemp1,
    //        borderWidth = "4"
    //      });
    //      dataSet.Add(new Datasets()
    //      {
    //        label = labelTemp2,
    //        data = dataTemp2,
    //        backgroundColor = backgroundcolorTemp2,
    //        borderColor = bordercolorTemp2,
    //        borderWidth = "4"
    //      });
    //      dataSet.Add(new Datasets()
    //      {
    //        label = labelTemp3,
    //        data = dataTemp3,
    //        backgroundColor = backgroundcolorTemp3,
    //        borderColor = bordercolorTemp3,
    //        borderWidth = "4"
    //      });
    //      break;

    //    case 4:

    //      dataSet.Add(new Datasets()
    //      {
    //        label = labelTemp1,
    //        data = dataTemp1,
    //        backgroundColor = backgroundcolorTemp1,
    //        borderColor = bordercolorTemp1,
    //        borderWidth = "4"
    //      });
    //      dataSet.Add(new Datasets()
    //      {
    //        label = labelTemp2,
    //        data = dataTemp2,
    //        backgroundColor = backgroundcolorTemp2,
    //        borderColor = bordercolorTemp2,
    //        borderWidth = "4"
    //      });
    //      dataSet.Add(new Datasets()
    //      {
    //        label = labelTemp3,
    //        data = dataTemp3,
    //        backgroundColor = backgroundcolorTemp3,
    //        borderColor = bordercolorTemp3,
    //        borderWidth = "4"
    //      });
    //      dataSet.Add(new Datasets()
    //      {
    //        label = labelTemp4,
    //        data = dataTemp4,
    //        backgroundColor = backgroundcolorTemp4,
    //        borderColor = bordercolorTemp4,
    //        borderWidth = "4"
    //      });
    //      break;

    //    case 5:

    //      dataSet.Add(new Datasets()
    //      {
    //        label = labelTemp1,
    //        data = dataTemp1,
    //        backgroundColor = backgroundcolorTemp1,
    //        borderColor = bordercolorTemp1,
    //        borderWidth = "4"
    //      });
    //      dataSet.Add(new Datasets()
    //      {
    //        label = labelTemp2,
    //        data = dataTemp2,
    //        backgroundColor = backgroundcolorTemp2,
    //        borderColor = bordercolorTemp2,
    //        borderWidth = "4"
    //      });
    //      dataSet.Add(new Datasets()
    //      {
    //        label = labelTemp3,
    //        data = dataTemp3,
    //        backgroundColor = backgroundcolorTemp3,
    //        borderColor = bordercolorTemp3,
    //        borderWidth = "4"
    //      });
    //      dataSet.Add(new Datasets()
    //      {
    //        label = labelTemp4,
    //        data = dataTemp4,
    //        backgroundColor = backgroundcolorTemp4,
    //        borderColor = bordercolorTemp4,
    //        borderWidth = "4"
    //      });
    //      dataSet.Add(new Datasets()
    //      {
    //        label = labelTemp5,
    //        data = dataTemp5,
    //        backgroundColor = backgroundcolorTemp5,
    //        borderColor = bordercolorTemp5,
    //        borderWidth = "4"
    //      });

    //      break;

    //  }


    //  chart.datasets = dataSet;
    //  return Json(chart, JsonRequestBehavior.AllowGet);
    //}



    //  public virtual JsonResult MaakGrafiekMeerdereDatasets(int aantalDatasets, string[] labels, string label1, int[] data1, string[] backgroundcolor1, string[] bordercolor1,)
    //  {

    //    Chart chart = new Chart
    //    {
    //      labels = labels,
    //      datasets = new List<Datasets>()
    //    };


    //    List<Datasets> dataSet = new List<Datasets>
    //    {
    //     new Datasets()
    //      {
    //        label = label1,
    //        data = data1,
    //        borderColor = bordercolor1,
    //        backgroundColor = backgroundcolor1,
    //        borderWidth = "4"
    //      },
    //      new Datasets()
    //      {
    //        label = label2,
    //        data = data2,
    //        borderColor = bordercolor2,
    //        backgroundColor = backgroundcolor2,
    //        borderWidth = "4"
    //      },

    //    };

    //    chart.datasets = dataSet;
    //    return Json(chart, JsonRequestBehavior.AllowGet);
    //  }

    //  public virtual JsonResult MaakGrafiek3Datasets(string[] labels,
    //  string label1, int[] data1, string[] backgroundcolor1, string[] bordercolor1,
    //  string label2, int[] data2, string[] backgroundcolor2, string[] bordercolor2,
    //  string label3, int[] data3, string[] backgroundcolor3, string[] bordercolor3

    //  )
    //  {

    //    Chart chart = new Chart
    //    {
    //      labels = labels,
    //      datasets = new List<Datasets>()
    //    };


    //    List<Datasets> dataSet = new List<Datasets>
    //    {
    //      new Datasets()
    //      {
    //        label = "Item 1",
    //        data = new int[] { 28, 48, 40, 19, 86, 27, 90 },

    //        //borderColor = new string[] { "#000000" },
    //        //backgroundColor = new string[] { "#FF0000" },
    //        borderColor = new string[] { "#FF0000" },
    //        //backgroundColor = new string[] { "#000000" },

    //        borderWidth = "4"
    //      },
    //      new Datasets()
    //      {
    //        label = "Item 2",
    //        data = new int[] { 65, 59, 80, 81, 56, 55, 40 },

    //        //borderColor = new string[] { "#000000" },
    //        //backgroundColor = new string[] { "#800000" },
    //        borderColor = new string[] { "#800000" },
    //        //backgroundColor = new string[] { "#000000" },

    //        borderWidth = "4"
    //      },
    //      new Datasets()
    //      {
    //        label = "Item 3",
    //        data = new int[] { 34, 88, 77, 66, 55, 40, 15 },

    //        //borderColor = new string[] { "#000000" },
    //        //backgroundColor = new string[] { "#808000" },
    //        borderColor = new string[] { "#808000" },
    //        //backgroundColor = new string[] { "#000000" },

    //        borderWidth = "4"
    //      }
    //    };

    //    chart.datasets = dataSet;
    //    return Json(chart, JsonRequestBehavior.AllowGet);
    //  }

    //  public virtual JsonResult MaakGrafiek4Datasets(string[] labels,
    //  string label1, int[] data1, string[] backgroundcolor1, string[] bordercolor1,
    //  string label2, int[] data2, string[] backgroundcolor2, string[] bordercolor2,
    //  string label3, int[] data3, string[] backgroundcolor3, string[] bordercolor3,
    //  string label4, int[] data4, string[] backgroundcolor4, string[] bordercolor4
    //  )
    //  {

    //    Chart chart = new Chart
    //    {
    //      labels = labels,
    //      datasets = new List<Datasets>()
    //    };


    //    List<Datasets> dataSet = new List<Datasets>
    //    {
    //      new Datasets()
    //      {
    //        label = "Item 1",
    //        data = new int[] { 28, 48, 40, 19, 86, 27, 90 },

    //        //borderColor = new string[] { "#000000" },
    //        //backgroundColor = new string[] { "#FF0000" },
    //        borderColor = new string[] { "#FF0000" },
    //        //backgroundColor = new string[] { "#000000" },

    //        borderWidth = "4"
    //      },
    //      new Datasets()
    //      {
    //        label = "Item 2",
    //        data = new int[] { 65, 59, 80, 81, 56, 55, 40 },

    //        //borderColor = new string[] { "#000000" },
    //        //backgroundColor = new string[] { "#800000" },
    //        borderColor = new string[] { "#800000" },
    //        //backgroundColor = new string[] { "#000000" },

    //        borderWidth = "4"
    //      },
    //      new Datasets()
    //      {
    //        label = "Item 3",
    //        data = new int[] { 34, 88, 77, 66, 55, 40, 15 },

    //        //borderColor = new string[] { "#000000" },
    //        //backgroundColor = new string[] { "#808000" },
    //        borderColor = new string[] { "#808000" },
    //        //backgroundColor = new string[] { "#000000" },

    //        borderWidth = "4"
    //      },
    //      new Datasets()
    //      {
    //        label = "Item 4",
    //        data = new int[] { 20, 45, 65, 34, 22, 95, 33 },

    //        //borderColor = new string[] { "#000000" },
    //        //backgroundColor = new string[] { "#008080" },
    //        borderColor = new string[] { "#008080" },
    //        //backgroundColor = new string[] { "#000000" },

    //        borderWidth = "4"
    //      }
    //    };

    //    chart.datasets = dataSet;
    //    return Json(chart, JsonRequestBehavior.AllowGet);
    //  }



    //  public virtual JsonResult MaakGrafiek5Datasets(string[] labels, int[] data, string[] backgroundcolor, string[] bordercolor)
    //  {

    //    Chart chart = new Chart
    //    {
    //      labels = labels,
    //      datasets = new List<Datasets>()
    //    };


    //    List<Datasets> dataSet = new List<Datasets>
    //    {
    //      new Datasets()
    //      {
    //        label = "Item 1",
    //        data = new int[] { 28, 48, 40, 19, 86, 27, 90 },

    //        //borderColor = new string[] { "#000000" },
    //        //backgroundColor = new string[] { "#FF0000" },
    //        borderColor = new string[] { "#FF0000" },
    //        //backgroundColor = new string[] { "#000000" },

    //        borderWidth = "4"
    //      },
    //      new Datasets()
    //      {
    //        label = "Item 2",
    //        data = new int[] { 65, 59, 80, 81, 56, 55, 40 },

    //        //borderColor = new string[] { "#000000" },
    //        //backgroundColor = new string[] { "#800000" },
    //        borderColor = new string[] { "#800000" },
    //        //backgroundColor = new string[] { "#000000" },

    //        borderWidth = "4"
    //      },
    //      new Datasets()
    //      {
    //        label = "Item 3",
    //        data = new int[] { 34, 88, 77, 66, 55, 40, 15 },

    //        //borderColor = new string[] { "#000000" },
    //        //backgroundColor = new string[] { "#808000" },
    //        borderColor = new string[] { "#808000" },
    //        //backgroundColor = new string[] { "#000000" },

    //        borderWidth = "4"
    //      },
    //      new Datasets()
    //      {
    //        label = "Item 4",
    //        data = new int[] { 20, 45, 65, 34, 22, 95, 33 },

    //        //borderColor = new string[] { "#000000" },
    //        //backgroundColor = new string[] { "#008080" },
    //        borderColor = new string[] { "#008080" },
    //        //backgroundColor = new string[] { "#000000" },

    //        borderWidth = "4"
    //      },
    //      new Datasets()
    //      {
    //        label = "Item 5",
    //        data = new int[] { 55, 66, 77, 88, 34, 99, 44 },

    //        //borderColor = new string[] { "#000000" },
    //        //backgroundColor = new string[] { "#800080" },
    //        borderColor = new string[] { "#800080" },
    //        //backgroundColor = new string[] { "#000000" },

    //        borderWidth = "4"
    //      }
    //    };

    //    chart.datasets = dataSet;
    //    return Json(chart, JsonRequestBehavior.AllowGet);
    //  }
    //}


    public virtual ActionResult LaadGrafiekenPartialView()
    {
      return PartialView("~/Views/Shared/Grafieken/Grafieken.cshtml");
    }


  }
}

