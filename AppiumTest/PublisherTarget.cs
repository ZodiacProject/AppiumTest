using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppiumTest
{
    class PublisherTarget
    {
        public string Url;
        public string ZoneId;
        public string TargetClick;
        public int CountShowPopup;
        public int Interval;
        public int StepCase;
        public int FrameNumber;
        public List<PublisherTarget> DriverSetting;

        public List <PublisherTarget> GetDriverSettings(string typeTest)
        {
            if (typeTest == "onclick")
            {
                DriverSetting = new List<PublisherTarget>()
                   {
                    new PublisherTarget() { Url = "http://putlocker.is", ZoneId = "10802", CountShowPopup = 3, Interval = 10000, StepCase = 0},
                    new PublisherTarget() { Url = "http://thevideos.tv/", ZoneId = "90446", CountShowPopup = 3, Interval = 45000, TargetClick = "morevids", StepCase = 1},              
                    new PublisherTarget() { Url = "http://www13.zippyshare.com/v/94311818/file.html/", ZoneId = "180376", CountShowPopup = 2, Interval = 45000, StepCase = 2},
                    new PublisherTarget() { Url = "http://um-fabolous.blogspot.ru/", ZoneId = "199287", CountShowPopup = 3, Interval = 45000, StepCase = 3},                
                    new PublisherTarget() { Url = "http://www.flashx.tv/&?", ZoneId = "119133", CountShowPopup = 1, Interval = 20000, StepCase = 4},              
                    };
                return DriverSetting;
            }
            if (typeTest == "pushup")
            {
                DriverSetting = new List<PublisherTarget>()
                   {
                    new PublisherTarget() { Url = "http://putlocker.is", StepCase = 0, FrameNumber = 1, Interval = 5000},
                    //new PublisherTarget() { Url = "http://audiopoisk.com", StepCase = 1, FrameNumber = 2, Interval = 5000},              
                    //new PublisherTarget() { Url = "http://exbii.com", StepCase = 2, FrameNumber = 2, Interval = 5000},
                    //new PublisherTarget() { Url = "http://nontonmovie.com/", StepCase = 3, FrameNumber = 1, Interval = 5000}, 
                    //new PublisherTarget() { Url = "http://kickass.to/", StepCase = 4, FrameNumber = 1, Interval = 5000},      
                    //new PublisherTarget() { Url = "http://www.solarmovie.is/", StepCase = 5, FrameNumber = 1, Interval = 5000},      
                    //new PublisherTarget() { Url = "http://um-fabolous.blogspot.ru/", StepCase = 6, FrameNumber = 3, Interval = 15000},              
                    };
                return DriverSetting;
            }
            else
                return null;
        }
        
    }  
}
