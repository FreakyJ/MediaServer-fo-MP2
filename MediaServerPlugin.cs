using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MediaPortal.Backend.BackendServer;
using MediaPortal.Common;
using MediaPortal.Common.Logging;
using MediaPortal.Common.PluginManager;
using MediaPortal.Common.ResourceAccess;
using MediaPortal.Extensions.MediaServer.Objects.MediaLibrary;
using MediaPortal.Extensions.MediaServer.Objects.Basic;
using MediaPortal.Extensions.MediaServer.ResourceAccess;

namespace MediaPortal.Extensions.MediaServer
{
    public class MediaServerPlugin : IPluginStateTracker
    {
        private UPnPMediaServerDevice _device;

        public const string DEVICE_UUID = "45F2C54D-8C0A-4736-AA04-E6F91CD45457";

        public static BasicContainer RootContainer { get; private set; }

        public MediaServerPlugin()
        {            
            _device = new UPnPMediaServerDevice(DEVICE_UUID.ToLower());

            InitialiseContainerTree();
        }

        private static void InitialiseContainerTree()
        {
            RootContainer = new BasicContainer("0") { Title = "MediaPortal Media Library" };
            var audioContainer = new BasicContainer("A") { Title = "Audio" };
            RootContainer.Add(audioContainer);
            var pictureContainer = new BasicContainer("P") { Title = "Picture" };
            RootContainer.Add(pictureContainer);
            var videoContainer = new BasicContainer("V") { Title = "Video" };
            RootContainer.Add(videoContainer);
                videoContainer.Add(new MediaLibraryGenreContainer("VG") { Title = "Genres" });
            RootContainer.Add(new MediaLibraryShareContainer("S") { Title = "Shares" });                        
        }

        public void Activated(PluginRuntime pluginRuntime)
        {
            var meta = pluginRuntime.Metadata;
            Logger.Info(string.Format("{0} v{1} [{2}] by {3}", meta.Name, meta.PluginVersion, meta.Description, meta.Author));

            Logger.Debug("MediaServerPlugin: Adding UPNP device as a root device");
            ServiceRegistration.Get<IBackendServer>().UPnPBackendServer.AddRootDevice(_device);
            ServiceRegistration.Get<IResourceServer>().AddHttpModule(new DlnaResourceAccessModule());
        }

        public bool RequestEnd()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void Continue()
        {
            throw new NotImplementedException();
        }

        public void Shutdown()
        {
            throw new NotImplementedException();
        }

        internal static ILogger Logger
        {
            get { return ServiceRegistration.Get<ILogger>(); }            
        }
    }
}
