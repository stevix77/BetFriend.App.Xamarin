using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;

namespace BetFriend.MobileApp.Views.EditBet
{
    internal class EditBetViewModel : ViewModelBase
    {
        public string Id { get; internal set; }
        public string Description { get; internal set; }
        public int Coins { get; internal set; }
        public DateTime EndDate { get; internal set; }

    }
}
