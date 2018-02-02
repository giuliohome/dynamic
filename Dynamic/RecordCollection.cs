using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Media;
using System.Linq;

namespace Dynamic
{
class RecordCollection:ObservableCollection<Record>
    {
     
        public RecordCollection(List<Bar> barvalues)
        {
            Random rand = new Random();
            BrushCollection brushcoll=new BrushCollection();
            double scale = barvalues.Max(b=>b.Value);
            foreach (Bar barval in barvalues)
            {
                int num = rand.Next(brushcoll.Count/3);
                Add(new Record(barval.Value,brushcoll[num], barval.BarName,scale));               
            }            
        }
       
    }

    class BrushCollection:ObservableCollection<Brush>
    {
        public BrushCollection()
        {
            Type _brush = typeof(Brushes);
            PropertyInfo []props=  _brush.GetProperties();
            foreach (PropertyInfo prop in props)
            {
                Brush _color = (Brush)prop.GetValue(null, null);
                if ( _color!=Brushes.LightSteelBlue && _color != Brushes.White && 
                     _color != Brushes.WhiteSmoke && _color!=Brushes.LightCyan &&      
                     _color!=Brushes.LightYellow && _color!=Brushes.Linen)
                Add(_color);
            }
        }
    }

    class Bar
    {
       
        public string BarName { set; get; }

        public double Value { set; get; }

    }
	
    class Record:INotifyPropertyChanged
    {
        public Brush Color { set; get; }

        public string Name { set; get; }

        private double _data;
        public double Data
        {
            set
            {
                if (_data != value)
                {
                    _data = value;
                  
                }
            }
            get
            {
                return _data;
            }
        }
        internal double scale;
        internal const double maxheight = 200;
        public double ScaledData
        {
        	get { return maxheight*_data/scale;}
        	private set {}
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Record(double value,Brush color,string name, double barscale)
        {
            Data = value;
            Color = color;
            Name = name;
            scale = barscale;
        }

        protected void PropertyOnChange(string propname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propname));
            }
        }
    }
}
