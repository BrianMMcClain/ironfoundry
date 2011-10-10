﻿namespace CloudFoundry.Net.Vmc.Cli
{
    using System;
    using NDesk.Options;

    static partial class Program
    {
        static bool Usage(OptionSet options = null)
        {
            if (null != options)
            {
                Console.WriteLine("Options:");
                options.WriteOptionDescriptions(Console.Out);
                Console.WriteLine();
            }
            Console.WriteLine(UsageStrings.COMMAND_USAGE);
            return true;
        }
    }

    public static class UsageStrings
    {
        public static readonly string BASIC_USAGE = @"Usage: vmc [options] command [<args>] [command_options]
Try 'vmc help [command]' or 'vmc help options' for more information.";

        public static readonly string COMMAND_USAGE = BASIC_USAGE + @"

Currently available vmc commands are:

  Getting Started
    target [url]                                 Reports current target or sets a new target
    login  [email] [--email, --passwd]           Login
    info                                         System and account information

  Applications
    apps                                         List deployed applications

  Application Creation
    push <appname> <path> <url>                  Create, push, map, and start a new application

  Application Operations
    delete <appname>                             Delete the application

  Application Updates
    update <appname> <path>                      Update the application bits

  Application Information
    files <appname> <path>                       Display directory listing or file download for path

  Services
    services                                     Lists of services available and provisioned
    create-service <service> <servicename>       Create a provisioned service and assign it <servicename>
    delete-service <servicename>                 Delete a provisioned service
    bind-service <servicename> <appname>         Bind a service to an application
    unbind-service <servicename> <appname>       Unbind service from the application

  Administration
    passwd                                       Change the password for the current user
    add-user                                     Register a new user (requires admin privileges)

  Help
    help                                         Get general help
";

        public static readonly string FULL_COMMAND_USAGE = BASIC_USAGE + @"

Currently available vmc commands are:

  Getting Started
    target [url]                                 Reports current target or sets a new target
    login  [email] [--email, --passwd]           Login
    info                                         System and account information

  Applications
    apps                                         List deployed applications

  Application Creation
    push [appname]                               Create, push, map, and start a new application
    push [appname] --path                        Push application from specified path
    push [appname] --url                         Set the url for the application
    push [appname] --instances <N>               Set the expected number <N> of instances
    push [appname] --mem M                       Set the memory reservation for the application
    push [appname] --runtime RUNTIME             Set the runtime to use for the application
    push [appname] --no-start                    Do not auto-start the application

  Application Operations
    start <appname>                              Start the application
    stop  <appname>                              Stop the application
    restart <appname>                            Restart the application
    delete <appname>                             Delete the application
    rename <appname> <newname>                   Rename the application

  Application Updates
    update <appname> [--path]                    Update the application bits
    mem <appname> [memsize]                      Update the memory reservation for an application
    map <appname> <url>                          Register the application to the url
    unmap <appname> <url>                        Unregister the application from the url
    instances <appname> <num|delta>              Scale the application instances up or down

  Application Information
    crashes <appname>                            List recent application crashes
    crashlogs <appname>                          Display log information for crashed applications
    logs <appname> [--all]                       Display log information for the application
    files <appname> [path] [--all]               Display directory listing or file download for [path]
    stats <appname>                              Display resource usage for the application
    instances <appname>                          List application instances

  Application Environment
    env <appname>                                List application environment variables
    env-add <appname> <variable[=]value>         Add an environment variable to an application
    env-del <appname> <variable>                 Delete an environment variable to an application

  Services
    services                                     Lists of services available and provisioned
    create-service <service> [--name,--bind]     Create a provisioned service
    create-service <service> <name>              Create a provisioned service and assign it <name>
    create-service <service> <name> <app>        Create a provisioned service and assign it <name>, and bind to <app>
    delete-service [servicename]                 Delete a provisioned service
    bind-service <servicename> <appname>         Bind a service to an application
    unbind-service <servicename> <appname>       Unbind service from the application
    clone-services <src-app> <dest-app>          Clone service bindings from <src-app> application to <dest-app>

  Administration
    user                                         Display user account information
    passwd                                       Change the password for the current user
    logout                                       Logs current user out of the target system
    add-user [--email, --passwd]                 Register a new user (requires admin privileges)
    delete-user <user>                           Delete a user and all apps and services (requires admin privileges)

  System
    runtimes                                     Display the supported runtimes of the target system
    frameworks                                   Display the recognized frameworks of the target system

  Misc
    aliases                                      List aliases
    alias <alias[=]command>                      Create an alias for a command
    unalias <alias>                              Remove an alias
    targets                                      List known targets and associated authorization tokens

  Help
    help [command]                               Get general help or help on a specific command
    help options                                 Get help on available options
";
    }
}