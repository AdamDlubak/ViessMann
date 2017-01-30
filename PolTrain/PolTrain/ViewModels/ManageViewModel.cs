using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PolTrain.Models;

namespace PolTrain.ViewModels
{
    public class ManageViewModel
    {
        public class ShowStationsViewModel
        {
            public IEnumerable<Station> Stations { get; set; }

        }
        public class ShowTracksViewModel
        {
            public IEnumerable<Track> Tracks { get; set; }
            public IEnumerable<Station> Stations { get; set; }
        }
        public class ShowLinesViewModel
        {
            public IEnumerable<Station> Stations { get; set; }
            public IEnumerable<Line> Lines { get; set; }
        }
        public class AddOrEditStationViewModel
        {
            public Station Station { get; set; }
            public bool EditMode { get; set; }
            public bool? ConfirmSuccess { get; set; }

        }
        public class AddOrEditTrackViewModel
        {
            public IEnumerable<Station> Stations { get; set; }
            public Track Track { get; set; }
            public bool EditMode { get; set; }
            public bool? ConfirmSuccess { get; set; }


        }
        public class AddOrEditLineViewModel
        {
            public IEnumerable<SelectListItem> Tracks { get; set; }
            public Line Line { get; set; }
            public bool EditMode { get; set; }
            public bool? ConfirmSuccess { get; set; }
            public IEnumerable<Station> Stations { get; set; }


        }
    }
}