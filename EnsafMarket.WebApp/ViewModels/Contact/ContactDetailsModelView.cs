using EnsafMarket.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EnsafMarket.WebApp.ViewModels
{
    public class ContactDetailsModelView : BaseViewModel
    {
        public Contact Contact { get; set; }
        
        public IEnumerable<ContactMessage> Messages { get; set; }

        public ContactFeedback OwnerFeedback { get; set; }

        public ContactFeedback UserFeedback { get; set; }

        public ContactFeedback MyFeedback
        {
            get
            {
                if (User == null)
                    return null;

                if (OwnerFeedback != null && User.Id == OwnerFeedback.FromUserId)
                    return OwnerFeedback;

                if (UserFeedback != null && User.Id == UserFeedback.FromUserId)
                    return UserFeedback;

                return null;
            }
        }

        public ContactFeedback OtherFeedback
        {
            get
            {
                if (User == null)
                    return null;

                if (OwnerFeedback != null && User.Id == OwnerFeedback.FromUserId)
                    return UserFeedback;

                if (UserFeedback != null && User.Id == UserFeedback.FromUserId)
                    return OwnerFeedback;

                return null;
            }
        }

        public string Action { get; set; }

        public string ContactMessage { get; set; }

        public int Rating { get; set; }

        public string Feedback { get; set; }

        public string MessageErrorMessage { get; set; }

        public string FeedbackErrorMessage { get; set; }
    }
}