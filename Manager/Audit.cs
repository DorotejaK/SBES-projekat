﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Manager
{
    public class Audit
    //public class Audit:IDisposable
    {

      //  private static EventLog customLog = null;
      //  const string SourceName = "SecurityManager.Audit";
      //  const string LogName = "MySecTest";
      //
      //  static Audit()
      //  {
      //      try
      //      {
      //          if (!EventLog.SourceExists(SourceName))
      //          {
      //              EventLog.CreateEventSource(SourceName, LogName);
      //          }
      //          customLog = new EventLog(LogName,
      //              Environment.MachineName, SourceName);
      //      }
      //      catch (Exception e)
      //      {
      //          customLog = null;
      //          Console.WriteLine("Error while trying to create log handle. Error = {0}", e.Message);
      //      }
      //  }
      //
      //
      //  public static void AuthenticationSuccess(string userName)
      //  {
      //   
      //
      //      if (customLog != null)
      //      {
      //          string UserAuthenticationSuccess =
      //              AuditEvents.AuthenticationSuccess;
      //          string message = String.Format(UserAuthenticationSuccess,
      //              userName);
      //          customLog.WriteEntry(message);
      //      }
      //      else
      //      {
      //          throw new ArgumentException(string.Format("Error while trying to write event (eventid = {0}) to event log.",
      //              (int)AuditEventTypes.AuthenticationSuccess));
      //      }
      //  }
      //
      //  public static void AuthorizationSuccess(string userName, string serviceName)
      //  {
      //      //TO DO
      //      if (customLog != null)
      //      {
      //          string AuthorizationSuccess =
      //              AuditEvents.AuthorizationSuccess;
      //          string message = String.Format(AuthorizationSuccess,
      //              userName, serviceName);
      //          customLog.WriteEntry(message);
      //      }
      //      else
      //      {
      //          throw new ArgumentException(string.Format("Error while trying to write event (eventid = {0}) to event log.",
      //              (int)AuditEventTypes.AuthorizationSuccess));
      //      }
      //  }
      //
      //  /// <summary>
      //  /// 
      //  /// </summary>
      //  /// <param name="userName"></param>
      //  /// <param name="serviceName"> should be read from the OperationContext as follows: OperationContext.Current.IncomingMessageHeaders.Action</param>
      //  /// <param name="reason">permission name</param>
      //
      //  public static void AuthenticationFailed(string userName)
      //  {
      //
      //
      //      if (customLog != null)
      //      {
      //          string UserAuthenticationFailed =
      //              AuditEvents.AuthenticationFailed;
      //          string message = String.Format(UserAuthenticationFailed,
      //              userName);
      //          customLog.WriteEntry(message);
      //      }
      //      else
      //      {
      //          throw new ArgumentException(string.Format("Error while trying to write event (eventid = {0}) to event log.",
      //              (int)AuditEventTypes.AuthenticationFailed));
      //      }
      //  }
      //
      //
      //  public static void AuthorizationFailed(string userName, string serviceName, string reason)
      //  {
      //      if (customLog != null)
      //      {
      //          string AuthorizationFailed =
      //              AuditEvents.AuthorizationFailed;
      //          string message = String.Format(AuthorizationFailed,
      //              userName, serviceName, reason);
      //          customLog.WriteEntry(message);
      //      }
      //      else
      //      {
      //          throw new ArgumentException(string.Format("Error while trying to write event (eventid = {0}) to event log.",
      //              (int)AuditEventTypes.AuthorizationFailed));
      //      }
      //  }
      //
      //  public void Dispose()
      //  {
      //      if (customLog != null)
      //      {
      //          customLog.Dispose();
      //          customLog = null;
      //      }
      //  }
    }
}
