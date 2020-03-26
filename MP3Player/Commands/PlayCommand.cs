﻿using MP3Player.Interfaces.Commands;
using MP3Player.Interfaces.Helpers;
using MP3Player.Interfaces.Models;
using NAudio.Wave;
using System;
using System.Windows.Input;

namespace MP3Player.Commands
{
    public class PlayCommand : ICommand, IPlayCommand
    {
        private readonly ISongHelpers SongHelpers;
        private readonly IWavePlayer WavePlayer;


        public PlayCommand(ISongHelpers songHelpers, IWavePlayer wavePlayer)
        {
            SongHelpers = songHelpers;
            WavePlayer = wavePlayer;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return SongHelpers.CanPlay(parameter as IPlaylist);
        }

        public void Execute(object parameter)
        {
            SongHelpers.Play(parameter as IPlaylist, WavePlayer);
        }
    }
}