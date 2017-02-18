using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TestConsole
{
    public sealed class Blip
    {
        #region vars
        private readonly DateTime _dateTime;
        private readonly decimal _price;
        private readonly int _size;
        private readonly bool _discontinuous;
        #endregion
        #region constructor
        public Blip(DateTime dateTime, decimal value, int size, bool discontinuous)
        {
            _dateTime = dateTime;
            _price = value;
            _size = size;
            _discontinuous = discontinuous;
        }
        public Blip(Blip model)
        {
            _dateTime = model._dateTime;
            _price = model._price;
            _size = model._size;
            _discontinuous = model._discontinuous;
        }
        #endregion
        #region properties
        public DateTime DateTime { get { return _dateTime; } }
        public decimal Price { get { return _price; } }
        public int Size { get { return _size; } }
        public bool Discontinuous { get { return _discontinuous; } }
        #endregion
        #region methods
        public override string ToString() { return string.Format("{0}: {1}@{2}", _dateTime.ToString("yyyy-MM-dd HH:mm:ss"), _size, _price.ToString("0.####")); }
        public Blip Clone() { return new Blip(_dateTime, _price, _size, _discontinuous); }
        #endregion
    }

    class Program
    {
        static int _MAX_LENGHT_ = 3000, _NB_ITERATIONS_ = 10, _NB_INSTRUMENTS_ = 1000;

        static void AddTest()
        {
            Stopwatch sw = new Stopwatch();
            #region initialization list
            List<List<Blip>> listBlips = new List<List<Blip>>(_NB_INSTRUMENTS_);
            for (int i = 0; i < _NB_INSTRUMENTS_; i++)
                listBlips.Add(new List<Blip>(_MAX_LENGHT_));
            #endregion
            #region adding into list
            sw.Start();
            for (int j = 0; j < _NB_INSTRUMENTS_; j++)
                for (int k = 0; k < _MAX_LENGHT_; k++)
                    listBlips[j].Add(new Blip(DateTime.Now, 100, 10, false));
            listBlips.Clear(); GC.Collect();
            sw.Stop();
            Console.WriteLine("Add into list = " + sw.Elapsed.ToString("hh':'mm':'ss'.'fffff"));
            #endregion

            #region initialization array
            Blip[][] arrayBlips = new Blip[_NB_INSTRUMENTS_][];
            for (int i = 0; i < _NB_INSTRUMENTS_; i++)
                arrayBlips[i] = new Blip[_MAX_LENGHT_];
            #endregion
            #region adding into array
            sw.Restart();
            for (int j = 0; j < _NB_INSTRUMENTS_; j++)
                for (int k = 0; k < _MAX_LENGHT_; k++)
                    arrayBlips[j][k] = new Blip(DateTime.Now, 100, 10, false);
            sw.Stop();
            Array.Clear(arrayBlips, 0, arrayBlips.Length); GC.Collect();
            Console.WriteLine("Add into array = " + sw.Elapsed.ToString("hh':'mm':'ss'.'fffff"));
            #endregion

            #region initialization dictionary
            Dictionary<int, List<Blip>> dicoBlips = new Dictionary<int, List<Blip>>();
            for (int i = 0; i < _NB_INSTRUMENTS_; i++)
                dicoBlips.Add(i, new List<Blip>(_MAX_LENGHT_));
            #endregion
            #region adding into dico
            sw.Restart();
            for (int j = 0; j < _NB_INSTRUMENTS_; j++)
                for (int k = 0; k < _MAX_LENGHT_; k++)
                    dicoBlips[j].Add(new Blip(DateTime.Now, 100, 10, false));
            sw.Stop();
            Console.WriteLine("Add into dico list = " + sw.Elapsed.ToString("hh':'mm':'ss'.'fffff"));
            #endregion

            #region initialization dictionary
            Dictionary<int, Queue<Blip>> dicoQueueBlips = new Dictionary<int, Queue<Blip>>();
            for (int i = 0; i < _NB_INSTRUMENTS_; i++)
                dicoQueueBlips.Add(i, new Queue<Blip>(_MAX_LENGHT_));
            #endregion
            #region adding into dico
            sw.Restart();
            for (int j = 0; j < _NB_INSTRUMENTS_; j++)
                for (int k = 0; k < _MAX_LENGHT_; k++)
                    dicoQueueBlips[j].Enqueue(new Blip(DateTime.Now, 100, 10, false));
            sw.Stop();
            Console.WriteLine("Add into dico queue = " + sw.Elapsed.ToString("hh':'mm':'ss'.'fffff"));
            #endregion
        }

        static void CopyTest()
        {
            Stopwatch sw = new Stopwatch();
            #region initialization array
            Blip[][] arrayBlips = new Blip[_NB_INSTRUMENTS_][];
            for (int i = 0; i < _NB_INSTRUMENTS_; i++)
            {
                arrayBlips[i] = new Blip[_MAX_LENGHT_];
                for (int k = 0; k < _MAX_LENGHT_; k++)
                    arrayBlips[i][k] = new Blip(DateTime.Now, 100, 10, false);
            }
            Console.ReadKey();
            #endregion
            #region copy into array
            sw.Start();
            Blip[][] arrayBlipsCopy = new Blip[_NB_INSTRUMENTS_][];
            for (int i = 0; i < _NB_ITERATIONS_; i++)
            {
                arrayBlipsCopy[i] = new Blip[_MAX_LENGHT_];
                arrayBlips[i].CopyTo(arrayBlipsCopy[i], 0);
            }
            sw.Stop();
            Console.WriteLine("Copy into array = " + sw.Elapsed.ToString("hh':'mm':'ss'.'fffff"));
            #endregion
            #region copy into array
            sw.Restart();
            Blip[][] arrayBlipsClone = new Blip[_NB_INSTRUMENTS_][];
            for (int i = 0; i < _NB_ITERATIONS_; i++)
            {
                //arrayBlipsClone = arrayBlips.Clone();

                //arrayBlipsCopy[i] = new Blip[_MAX_LENGHT_];
                //arrayBlips[i].CopyTo(arrayBlipsCopy[i], 0);
            }
            sw.Stop();
            Console.WriteLine("Copy into array = " + sw.Elapsed.ToString("hh':'mm':'ss'.'fffff"));
            #endregion
        }

        static void DepopulateyTest()
        {
            Stopwatch sw = new Stopwatch();
            #region deep copy list
            Dictionary<int, List<Blip>> dicoBlips = new Dictionary<int, List<Blip>>();
            for (int i = 0; i < _NB_INSTRUMENTS_; i++)
            {
                dicoBlips.Add(i, new List<Blip>(_MAX_LENGHT_));
                for (int k = 0; k < _MAX_LENGHT_; k++)
                    dicoBlips[i].Add(new Blip(DateTime.Now, 100, 10, false));
            }
            Console.ReadKey();
            sw.Start();
            Dictionary<int, List<Blip>> dicoBlipsCopy = new Dictionary<int, List<Blip>>();
            foreach(KeyValuePair<int, List<Blip>> pair in dicoBlips)
            {
                List<Blip> blips = pair.Value;
                dicoBlipsCopy.Add(pair.Key, new List<Blip>(blips.Capacity));
                List<Blip> newBlips = dicoBlipsCopy[pair.Key];
                for (int i = 0; i < blips.Count; i++)
                    newBlips.Add(blips[i].Clone());
            }
            foreach (int key in dicoBlips.Keys) dicoBlips[key].Clear();
            sw.Stop();
            Console.WriteLine("Add deep copy list = " + sw.Elapsed.ToString("hh':'mm':'ss'.'fffff"));
            #endregion
            GC.Collect();
            Console.ReadKey();
            #region queue
            Dictionary<int, Queue<Blip>> dicoBlipsQueue = new Dictionary<int, Queue<Blip>>();
            for (int i = 0; i < _NB_INSTRUMENTS_; i++)
            {
                dicoBlipsQueue.Add(i, new Queue<Blip>(_MAX_LENGHT_));
                for (int k = 0; k < _MAX_LENGHT_; k++)
                    dicoBlipsQueue[i].Enqueue(new Blip(DateTime.Now, 100, 10, false));
            }
            Console.ReadKey();
            sw.Restart();
            Dictionary<int, Queue<Blip>> bufferQueue = new Dictionary<int, Queue<Blip>>();
            foreach (KeyValuePair<int, Queue<Blip>> pair in dicoBlipsQueue)
            {
                Queue<Blip> blips = pair.Value;
                bufferQueue.Add(pair.Key, new Queue<Blip>(blips.Count));
                Queue<Blip> newBlips = bufferQueue[pair.Key];
                while (blips.Count > 0)
                    newBlips.Enqueue(blips.Dequeue());
            }
            sw.Stop();
            Console.WriteLine("queue = " + sw.Elapsed.ToString("hh':'mm':'ss'.'fffff"));
            #endregion
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Test depopulate");
            Console.ReadKey();
            DepopulateyTest();
            Console.WriteLine("***** END ******");
            Console.WriteLine("nb elts =" + _NB_INSTRUMENTS_ * _NB_ITERATIONS_ * _MAX_LENGHT_);
            Console.ReadKey();
        }
    }
}
