using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventPractice
{
    class ObjChangeEventArgs : EventArgs
    {
        public string propChanged;
    }
    class Program
    {
        public delegate void myEventHandler(string value);

        class EventPublisher
        {
            private string theVal;

            public event myEventHandler valueChanged;
            public event EventHandler<ObjChangeEventArgs> objChanged;

            public string Val
            {
                set
                {
                    this.theVal = value;
                    this.valueChanged(theVal);
                    this.objChanged(this, new ObjChangeEventArgs { propChanged = "Val" });
                }
            }
        }
        static void Main(string[] args)
        {

            EventPublisher publisher = new EventPublisher();
            publisher.valueChanged += obj_valueChanged;
            publisher.valueChanged += changeListner1;
            publisher.valueChanged += changeListner2;
            publisher.valueChanged += delegate (string s)
            {

                Console.WriteLine($"This value {s} come to an annonymous delegate");
            };

            publisher.objChanged += delegate (object sender, ObjChangeEventArgs e)
              {
                  Console.WriteLine("{0} has the {1} property changed",sender.GetType(),e.propChanged);
              };

            publisher.valueChanged += (s) =>
            {
                Console.WriteLine($"This value {s} come to an Lambda function");
            };

            string theText;
            do
            {
                Console.WriteLine("enter some text: ");
                theText = Console.ReadLine();
                if (!theText.Equals("exit"))
                {
                    publisher.Val = theText;
                }
            } while (!theText.Equals("exit"));



        }
        static void obj_valueChanged(string value)
        {
            Console.WriteLine($"Value change to {value}");
        }
        static void changeListner1(string value)
        {
            Console.WriteLine($"I got the value {value}");
        }

        static void changeListner2(string value)
        {
            Console.WriteLine($"I also get updated with the value {value}");
        }
    }
}
