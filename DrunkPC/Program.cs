using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using System.Media;


namespace DrunkPC
{

    //Application that generates mouse and keyboard events to confuse user.

    class Program
    {
        public static Random _random = new Random();

        public static int _startupDelaySeconds = 10;
        public static int _totalDurationSeconds = 10;



        static void Main(string[] args)
        {
            Console.WriteLine("Drunk PC application");
            // check for command line arguments and assign the new values
            if (args.Length >= 2)
            {
                _startupDelaySeconds = Convert.ToInt32(args[0]);
                _startupDelaySeconds = Convert.ToInt32(args[1]);
            }


            Thread drunkMouseThread = new Thread(new ThreadStart(DrunkMouseThread));
            Thread drunkKeyboardThread = new Thread(new ThreadStart(DrunkKeyBoardThread));
            Thread drunkSoundThread = new Thread(new ThreadStart(DrunkSoundThread));
            Thread drunkPopUpThread = new Thread(new ThreadStart(DrunkPopUpThread));

            DateTime future = DateTime.Now.AddSeconds(_startupDelaySeconds);
            Console.WriteLine("Waiting Ten Seconds before starting threads");

            while (future > DateTime.Now)
            {
                Thread.Sleep(1000);
            }
            ///Start all Threads
            drunkMouseThread.Start();
            drunkKeyboardThread.Start();
            drunkSoundThread.Start();
            drunkPopUpThread.Start();

            future = DateTime.Now.AddSeconds(_totalDurationSeconds);
            while (future > DateTime.Now)
            {
                Thread.Sleep(1000);
            }

            ///wait for user input
            Console.Read();

            //kill all threads and exit app
            drunkMouseThread.Abort();
            drunkKeyboardThread.Abort();
            drunkSoundThread.Abort();
            drunkPopUpThread.Abort();
        }


        #region Thread Functions
        /// <summary>
        /// This will affect the mouse movements to annoy the user
        /// </summary>
        public static void DrunkMouseThread()
        {
            Console.WriteLine("Drunk Mouse Thread Started");

            int moveX = 0;
            int moveY = 0;

            while (true)
            {
                if (_random.Next(100) > 80)
                {
                    // Console.WriteLine(Cursor.Position.ToString());

                    //generate the random amount to move the cursor on X and Y Axis.
                    moveX = _random.Next(20) - 10;
                    moveY = _random.Next(20) - 10;

                    //change mouse cursor position to new random coordinates
                    Cursor.Position = new System.Drawing.Point(
                        Cursor.Position.X + moveX,
                        Cursor.Position.Y + moveY);
                }

                Thread.Sleep(50);
            }
        }

        ///This will generate random keyboard output to annoy user
        ///
        public static void DrunkKeyBoardThread()
        {
            Console.WriteLine("Drunk Keyboard Thread Started");

            while (true)
            {

                if (_random.Next(100) > 80)
                {
                    /// Generate a random capital letter
                    char key = (char)(_random.Next(25) + 65);

                    //make it lower case with a 50/50 distribution
                    if (_random.Next(2) == 0)
                    {
                        key = Char.ToLower(key);
                    }

                    SendKeys.SendWait(key.ToString());
                }
                    Thread.Sleep(_random.Next(500));
                
            }
        }

        /// <summary>
        /// This will play sounds to completely bamboozle the user
        /// </summary>
        public static void DrunkSoundThread()
        {
            Console.WriteLine("Drunk Sound Thread Started");

            while (true)
            {
                if (_random.Next(100) > 90)
                {
                    //randomly select a system sound
                    switch (_random.Next(5))
                    {
                        case 0:
                            SystemSounds.Asterisk.Play();
                            break;
                        case 1:
                            SystemSounds.Beep.Play();
                            break;
                        case 2:
                            SystemSounds.Exclamation.Play();
                            break;
                        case 3:
                            SystemSounds.Hand.Play();
                            break;
                        case 4:
                            SystemSounds.Question.Play();
                            break;
                    }
                }
                 
                Thread.Sleep(1000);
            }
        }
     
        /// <summary>
        /// This thread will pop up fake error notifications 
        /// </summary>
        public static void DrunkPopUpThread()
        {
            Console.WriteLine("Drunk Pop Up Thread Started");

            while (true)
            {
                if (_random.Next(100) > 90)
                {   
                    //determine which message to show user
                    
                    switch(_random.Next(2))
                    {
                        case 0:
                            MessageBox.Show(
                                "Internet Explorer has stopped working",
                                "Internet Explorer",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                            break;
                        case 1:
                            MessageBox.Show(
                                "Your system is running low on resources",
                                "Microsoft Windows",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                            break;
                    }
                }
                    
                Thread.Sleep(500);
            }
        }

        #endregion
    }
}
