using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    interface IDisk
    {
        string Read();
        void Write(string text);
    }

    interface IRemoveableDisk
    {
        bool HasdDisk { get; }
        void Insert();
        void Reject();
    }

    interface IPrintInformation
    {
        string GetName();
        void Print(string str);
    }

    class Disk : IDisk
    {
        protected string memory;
        protected int memSize;

        public string Memory { get; set; }
        public int MemSize { get; set; }

        public Disk() : this("No memory", 1) { }
        public Disk(string memory, int memSize)
        {
            this.memory = memory;
            this.memSize = memSize;
        }

        public virtual string GetName()
        {
            return "Some disk";
        }

        public string Read()
        {
            return "Reading...";
        }

        public void Write(string text)
        {
            Console.WriteLine($"Writing text: {text}");
        }

        public override string ToString()
        {
            return $"Memory - {Memory}. Memory size - {MemSize} MB";
        }
    }

    class CD : Disk, IRemoveableDisk
    {
        bool hasdDisk;
        public bool HasdDisk => hasdDisk;

        public override string GetName()
        {
            return "СD";
        }

        public void Insert()
        {
            Console.WriteLine("Inserting CD..");
        }

        public void Reject()
        {
            Console.WriteLine("Rejecting CD..");
        }

        public override string ToString()
        {
            return $"{GetName()}: {base.ToString()}";
        }
    }

    class Flash : Disk, IRemoveableDisk
    {
        bool hasdDisk;
        public bool HasdDisk => hasdDisk;

        public override string GetName()
        {
            return "Flash";
        }

        public void Insert()
        {
            Console.WriteLine("Inserting Flash..");
        }

        public void Reject()
        {
            Console.WriteLine("Rejecting Flash..");
        }

        public override string ToString()
        {
            return $"{GetName()}: {base.ToString()}";
        }
    }

    class HDD : Disk
    {
        public override string GetName()
        {
            return "HDD";
        }
    }

    class DVD : Disk, IRemoveableDisk
    {
        bool hasdDisk;
        public bool HasdDisk => hasdDisk;

        public override string GetName()
        {
            return "DVD";
        }

        public void Insert()
        {
            Console.WriteLine("Inserting DVD..");
        }

        public void Reject()
        {
            Console.WriteLine("Rejecting DVD..");
        }

        public override string ToString()
        {
            return $"{GetName()}: {base.ToString()}";
        }
    }

    class Printer : IPrintInformation
    {
        public string GetName()
        {
            return "Printer";
        }

        public void Print(string str)
        {
            Console.WriteLine($"Printing... \t{str}");
        }

        public override string ToString()
        {
            return GetName();
        }
    }

    class Monitor : IPrintInformation
    {
        public string GetName()
        {
            return "Monitor";
        }

        public void Print(string str)
        {
            Console.WriteLine($"Printing... \t{str}");
        }

        public override string ToString()
        {
            return GetName();
        }
    }

    class Comp
    {
        int countDisk;
        int countPrintDevice;
        Disk[] disks;
        IPrintInformation[] printDevice;

        public Comp(int d, int pd)
        {
            countDisk = d;
            countPrintDevice = pd;
            disks = new Disk[countDisk];
            printDevice = new IPrintInformation[countPrintDevice];
        }

        public void AddDevice(int index, IPrintInformation si)
        {
            if (printDevice[index] == null)
            {
                printDevice[index] = si;
                Console.WriteLine("Device wad added!");

            }
            else throw new Exception("This element is not empty! Can't add device\n");
        }

        public void AddDisk(int index, Disk d)
        {
            if (disks[index] == null)
            {
                disks[index] = d;
                Console.WriteLine("Disk wad added!");
            }

            else
                throw new Exception("This element is not empty! Can't add disk\n");
        }

        public bool CheckDisk(string device)
        {
            return device == "DVD" || device == "Flash" || device == "CD";
        }


        public void InsertReject(string device, bool b)
        {
            if(b)
                Console.WriteLine($"Inserting {device}");
            else
                Console.WriteLine($"Rejecting {device}");
        }

        public bool PrintInfo(string text, string device)
        {
            if (device == "busy" || device == "invalid")
            {
                return false;
            }
            else
            {
                Console.WriteLine($"Printing this text: {text}");
                return false;
            }
        }

        public string ReadInfo(string device)
        {
            return $"Reading info from device - {device}"; 
        }

        public void ShowDisk() 
        {
            Console.WriteLine("Show disk..");
        }

        public void ShowPrintDevice() 
        { 
            Console.WriteLine("Show print device..");

        }

        public bool WriteInfo(string text, string showDevice) 
        {
            if (showDevice != "busy" || showDevice != "invalid")
            {
                Console.WriteLine($"Writing this text: {text}");
                return true;
            }
            else
                return false;
        }

        public override string ToString()
        {
            return $"Count of disk - {countDisk}\nCount of printer device - {countPrintDevice}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Comp comp = new Comp(4, 2);

            Disk cd = new CD()
            {
                Memory = "CD",
                MemSize = 700
            };

            Disk dvd = new DVD()
            {
                Memory = "DVD",
                MemSize = 700
            };

            Disk flash = new Flash()
            {
                Memory = "Flash",
                MemSize = 16000
            };

            Disk hdd = new HDD()
            {
                Memory = "HDD",
                MemSize = 500000
            };

            try
            {
                comp.AddDisk(0, cd);
                comp.AddDisk(2, dvd);
                comp.AddDisk(1, hdd);
                comp.AddDisk(3, flash);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
               
            }
            Console.WriteLine();

            IPrintInformation printer = new Printer();
            IPrintInformation monitor = new Monitor();

            
            try
            {
                comp.AddDevice(0, printer);
               // comp.AddDevice(0, monitor); //error
                comp.AddDevice(1, monitor);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            Console.WriteLine();
            Console.WriteLine("Comp's info:\n" + comp.ToString());
            

            //zapys na dysk
            Console.WriteLine();
            comp.InsertReject(dvd.GetName(), true);

            if (comp.CheckDisk(dvd.GetName()))
            {               
                comp.ShowDisk();
                Console.WriteLine(comp.ReadInfo(dvd.GetName()) + "\n"+dvd.ToString());
                

                if (comp.WriteInfo("Hello, Vlad ;)", "OK"))
                    Console.WriteLine("Successfully writting!");
                else
                    Console.WriteLine("Writing has been canceled!");                    
            }

            comp.InsertReject(dvd.GetName(), false);
            Console.WriteLine();


            //druk na prynteri
            Console.Write(printer.GetName() + ": ");
            comp.ShowPrintDevice();            

            if (comp.PrintInfo("Hello, Vlad ;)", "invalid"))
                Console.WriteLine("Successfully printed!");
            else
                Console.WriteLine("Printing has been canceled!");



        }
    }
}
