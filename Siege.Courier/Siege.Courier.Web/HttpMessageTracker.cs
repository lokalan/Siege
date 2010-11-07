﻿using System;
using System.Collections.Generic;
using System.Web;
using Siege.Courier.Messages;

namespace Siege.Courier.Web
{
    public class HttpMessageTracker : IMessageTracker
    {
        public void Track(IMessage message)
        {
            var messages = (List<IMessage>)HttpContext.Current.Items["tracker"];
            messages = messages ?? new List<IMessage>();

            messages.Add(message);

            HttpContext.Current.Items.Add("tracker", messages);
        }

        public bool IsTracked(IMessage message)
        {
            var messages = (List<IMessage>)HttpContext.Current.Items["tracker"];
            messages = messages ?? new List<IMessage>();

            return messages.Contains(message);
        }

        public void Clear()
        {
            
        }
    }
}