using MP3Player.Enums;
using MP3Player.Interfaces;
using MP3Player.Models;
using NAudio.Wave;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MP3Player.Extensions
{
    public static class SongExtension
    {
        public static bool CanPlayMusic(this ISong song, IPlaylist playlist)
        {
            if (song == null || string.IsNullOrWhiteSpace(playlist?.SelectedSong))
                return false;
            return true;
        }

        public static bool CanPauseSong(this ISong song)
        {
            if (song == null || !song.IsPlaying)
                return false;
            return true;
        }

        public static void PlayMusic(this ISong song, IPlaylist playlist, WaveOut waveOut)
        {
            if (!song.IsPlaying && song.IsPausing && playlist.SelectedSong == song.Path)
            {
                waveOut.Play();
                song.IsPlaying = true;
                song.IsPausing = false;
                return;
            }
            else if (song.IsPlaying)
            {
                waveOut.Pause();
            }

            song.PlayerHelper(playlist, waveOut);
        }

        public static void SongPause(this ISong song, WaveOut waveOut)
        {
            waveOut.Pause();
            song.IsPlaying = false;
            song.IsPausing = true;
        }

        public static void UniversalPlay(this ISong song, IPlaylist playlist, PlayType playType, WaveOut waveOut)
        {
            if (song.IsPlaying)
            {
                waveOut.Pause();
                song.IsPausing = false;
            }

            playlist.SelectedSong = song.GetNewSongPath(playlist.SongsList, playType);
            song.PlayerHelper(playlist, waveOut);
        }

        private static void PlayerHelper(this ISong song, IPlaylist playlist, WaveOut waveOut)
        {
            if (!File.Exists(playlist.SelectedSong))
                playlist.SongsList.Remove(playlist.SelectedSong);

            if (!string.IsNullOrWhiteSpace(playlist.SelectedSong) && File.Exists(playlist.SelectedSong))
            {
                song = new Song(playlist.SelectedSong, song.Volume) { IsPlaying = true };
                song.PositionMax = song.MP3.TotalTime.TotalSeconds;

                song.CountTime((obj, e) =>
                {
                    if (song.MP3.CurrentTime == song.MP3.TotalTime && playlist.SongsList.FirstOrDefault() != null)
                    {
                        playlist.SelectedSong = song.GetNewSongPath(playlist.SongsList, PlayType.Next);
                        song.PlayerHelper(playlist, waveOut);
                    }

                    song.TimeText = string.Format("{0} {1}",
                        song.MP3.CurrentTime.ToString(@"hh\:mm\:ss"),
                        song.MP3.TotalTime.ToString().Split('.')[0]);

                    if (song.IsPlaying)
                        song.PositionValue = song.MP3.CurrentTime.TotalSeconds;
                });

                song.ChangePosition();
                song.Name = Path.GetFileName(song.MP3.FileName);
                song.MP3.Volume = song.Volume / 100;
                waveOut.Init(song.MP3);
                waveOut.Play();
            }
        }

        private static string GetNewSongPath(this ISong song, IList<string> songsList, PlayType playType)
        {
            var songsListWithIDs = songsList.Select((x, i) => new { Index = i, Value = x });
            var currentlyID = songsListWithIDs.First(x => x.Value == song.Path).Index;
            var ID = currentlyID == 0 ? songsList.Count - 1 : currentlyID - 1;

            if (playType == PlayType.Next)
                ID = currentlyID == songsList.Count - 1 ? 0 : currentlyID + 1;

            return songsListWithIDs.First(x => x.Index == ID).Value;
        }
    }
}