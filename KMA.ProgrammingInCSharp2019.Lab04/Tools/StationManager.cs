using System;
using System.Windows;

namespace KMA.ProgrammingInCSharp2019.Lab04.Tools
{
    internal static class StationManager
    {
        private static IDataStorage _dataStorage;

        internal static Person CurrentPerson { get; set; }

        internal static IDataStorage DataStorage
        {
            get { return _dataStorage; }
        }

        internal static void Initialize(IDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
        }
    }
}