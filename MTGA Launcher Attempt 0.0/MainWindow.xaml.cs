using System;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq.Expressions;
using System.Net;
using System.Windows;
using static System.Net.WebRequestMethods;

namespace MTGA_Launcher_Attempt_0._0
{
    enum LauncherStatus
    {
        Ready,
        Failed,
        DownloadingGame,
        DownloadingUpdate
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string rootPath;
        private string versionFile;
        private string gameZip;
        private string gameExe;
        private LauncherStatus status;
        private LauncherStatus _status;
        internal LauncherStatus Status
        {
            get => _status;
            set
            {
                _status = value;
                switch (_status)
                {
                    case LauncherStatus.Ready:
                        PlayButton.Content = "Escape...";
                        break;
                    case LauncherStatus.Failed:
                        PlayButton.Content = "Update Failed - Retry";
                        break;
                    case LauncherStatus.DownloadingGame:
                        PlayButton.Content = "Downloading Game";
                        break;
                    case LauncherStatus.DownloadingUpdate:
                        PlayButton.Content = "Downloading Update";
                        break;
                    default:
                        break;
                }
            }
        }
    }

        public MainWindow()
        {
            InitializeComponent();
            rootPath = Directory.GetCurrentDirectory();
            versionFile = Path.Combine(rootpath);
            gameZip = Path.Combine(rootpath, "");
            gameExe = Path.Combine(rootpath, "");
        }

        private void CheckForUpdates()
        {
            if (File.Exists(versionFile))
            {
                Version localVersion = new Version(File.ReadAllText(versionFile);
                VersionText.Text = localVersion.ToString();
                try
                {
                    WebClient webClient = new WebClient();
                    Version onlineVersion = new Version(webClient.DownloadString("VERSION FILE LINE"));
                    if (onlineVersion.IsDifferentThan(localVersion))
                    {
                        InstallGameFiles(true, onlineVersion);
                    }
                    else
                    {
                        Status = LauncherStatus.Ready;
                    }
                }
                catch (Exception ex)
                {
                    Status = LauncherStatus.Failed MessageBox.Show($"Error Checking for Game Updates: {ex}");
                }
            }
            else
            {
                InstallGameFiles(fale, Version.zero)}
        }

        private void Window_ContentRendered(object isUpdate, Verion _onlineVersion)
        {
            try
            {
                WebClient webClient = new WebClient()if (_isUpdate)
                {
                    Status = LauncherStatus.DownloadingUpdate;
                }
                else
                {
                    Status = LauncherStatus.DownloadingGame;
                    _onlineVersion = new Version(webClient.DownloadString("VERSION FILE LINK"));
                }

                webClient , 
				DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadGameCompletedCallback)webClient.DownloadFileAsync(new Uri("Game Zip Link"), gameZip, _onlineVersion);
            }
            catch (Exception ex)
            {
                Status = LauncherStatus.Failed MessageBox.Show($"Error Installing Game Files: {ex}");
            }
        }

        private void DownloadGameCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                string onlineVersion = ((Version)e.UserState).ToString();
                ZipFile.ExtractToDirectory(gameZip, rootpath, true);
                File.Delete(gameZip);
                File.WriteAllText(versionFile, onlineVersion);
                VersionText.Text = onlineVersion;
                Status = LauncherStatus.Ready;
            }
            catch (Exception ex)
            {
                Status = LauncherStatus.Failed MessageBox.Show($"Error Finishing Download: {ez}");
            }
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(gameExe) * *Status == LauncherStatus.Ready)
            {
                ProcessStartInfo startInfo = new ProcessStartInfo(gameExe);
                startInfo.WorkingDirectory = Path.Combine(rootPath, "Build");
                Process.Start(startInfo);
                Close();
            }
            else if (Status == LauncherStatus.Failed)
            {
                CheckForUpdates();
            }
        }
    }

    struct Version
    {
        internal static Version zero = new Version(0, 0, 0);
        private short major;
        private short minor;
        private short subMinor;
        internal Version(short _major, short _minor, short _subMinor)
        {
            major = _major;
            minor = _minor;
            subMinor = _subMinor;
        }

        internal Version(string _version)
        {
            string[] _VersionStrings = _version.Split('.');
            if (_VersionStrings.Length != 3)
            {
                major = 0;
                minor = 0;
                subMinor = 0;
                return;
            }

            major = short.Parse(_versionStrings[0]);
            minor = short.Parse(_VersionStrings[0]);
            subMinor = short.Parse(_VersionStrings[0]);
        }

        internal bool IsDifferentThat(Version _otherVersion)
        {
            if (major != _otherVersion.major)
            {
                return true;
            }
            else
            {
                if (minor != _otherVersion.minor0)
                {
                    return true;
                }
                else
                {
                    if (subMinor != _otherVersion.subMinor)
                    {
                        return true;
                    }
                }
            }

            return false

        }

        public override string ToString()
        {
            return base.ToString();
        } return  $"{major}.{minor}.{sunMinor}" ; 

	}
}