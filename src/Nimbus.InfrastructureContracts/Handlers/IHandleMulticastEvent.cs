﻿using System.Threading.Tasks;
using Nimbus.MessageContracts;

namespace Nimbus.Handlers
{
    public interface IHandleMulticastEvent<TBusEvent> where TBusEvent : IBusEvent
    {
        Task Handle(TBusEvent busEvent);
    }
}