using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Threading;

namespace KidsPexeso;

public partial class MainWindow : Window
{
    private static readonly string[] AnimalPictures =
    {
        "🐴", "🐔", "🐶", "🐱", "🐷", "🐮", "🐐", "🐓",
        "🕊️", "🐹", "🐊", "🦒", "🐻", "🐯", "🦁", "🐒",
        "🦍", "🦧", "🙈", "🙉", "🙊", "🦆", "🐘", "🦓",
        "🦛", "🦏", "🐪", "🦘", "🐼", "🐨", "🦊", "🐺"
    };
    private StackPanel _content = new();
    private TextBlock _status = new();
    private TextBlock _score = new();
    private TextBlock _timer = new();
    private readonly DispatcherTimer _gameTimer;
    private readonly Random _random = new();
    private readonly List<Card> _cards = new();
    private Button? _firstCard;
    private bool _checking;
    private int _boardSize;
    private int _scoreValue;
    private int _moves;
    private int _matchedPairs;
    private DateTime _startedAt;

    public MainWindow()
    {
        InitializeComponent();
        _gameTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
        _gameTimer.Tick += (_, _) => UpdateGameInfo();
        ShowStartScreen();
    }

    private void ShowStartScreen()
    {
        _gameTimer.Stop();
        _content = new StackPanel();
        _content.HorizontalAlignment = HorizontalAlignment.Center;
        _content.VerticalAlignment = VerticalAlignment.Center;
        _content.Spacing = 18;

        _content.Children.Add(new TextBlock
        {
            Text = "KidsPexeso",
            FontSize = 42,
            FontWeight = FontWeight.Bold,
            HorizontalAlignment = HorizontalAlignment.Center,
            Foreground = Brush.Parse("#243B53")
        });
        _content.Children.Add(new TextBlock
        {
            Text = "Vyber velikost hrací plochy",
            FontSize = 20,
            HorizontalAlignment = HorizontalAlignment.Center,
            Foreground = Brush.Parse("#486581")
        });

        var choices = new StackPanel
        {
            Orientation = Orientation.Horizontal,
            HorizontalAlignment = HorizontalAlignment.Center,
            Spacing = 12
        };
        foreach (var size in new[] { 4, 8 })
        {
            var button = CreateButton($"{size} × {size}", 140, 70, 24);
            button.Click += (_, _) => StartGame(size);
            choices.Children.Add(button);
        }

        _content.Children.Add(choices);
        Root.Children.Clear();
        Root.Children.Add(_content);
    }

    private void StartGame(int size)
    {
        _boardSize = size;
        _scoreValue = 0;
        _moves = 0;
        _matchedPairs = 0;
        _firstCard = null;
        _checking = false;
        _startedAt = DateTime.Now;
        _cards.Clear();

        var pairCount = size * size / 2;
        var values = Enumerable.Range(1, pairCount)
            .SelectMany(value => new[] { value, value })
            .OrderBy(_ => _random.Next())
            .ToList();

        for (var index = 0; index < values.Count; index++)
        {
            var picture = AnimalPictures[(values[index] - 1) % AnimalPictures.Length];
            _cards.Add(new Card(values[index], picture, index));
        }

        _status = new TextBlock();
        _score = new TextBlock();
        _timer = new TextBlock();
        _status.Text = $"Hra {size} × {size}";
        _status.FontSize = 20;
        _status.FontWeight = FontWeight.Bold;
        _status.Foreground = Brush.Parse("#243B53");
        _score.FontSize = 16;
        _timer.FontSize = 16;
        var sidebar = new StackPanel
        {
            Width = 205,
            Spacing = 16,
            Margin = new Thickness(18),
            VerticalAlignment = VerticalAlignment.Top
        };
        sidebar.Children.Add(_status);
        sidebar.Children.Add(_score);
        sidebar.Children.Add(_timer);

        var back = CreateButton("← Začátek", 125, 38, 15);
        back.Click += (_, _) => ShowStartScreen();
        sidebar.Children.Add(back);

        var board = new Grid
        {
            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center,
            Margin = new Thickness(10)
        };
        for (var row = 0; row < size; row++)
        {
            board.RowDefinitions.Add(new RowDefinition(GridLength.Auto));
            board.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Auto));
        }

        var cardSize = size switch
        {
            4 => 170,
            8 => 100,
            _ => 100
        };
        foreach (var card in _cards)
        {
            var button = CreateButton("×", cardSize, cardSize, 52);
            button.Tag = card;
            button.Click += CardClicked;
            Grid.SetRow(button, card.Index / size);
            Grid.SetColumn(button, card.Index % size);
            board.Children.Add(button);
            card.Button = button;
        }

        var scroll = new ScrollViewer
        {
            Content = board,
            HorizontalScrollBarVisibility = Avalonia.Controls.Primitives.ScrollBarVisibility.Auto,
            VerticalScrollBarVisibility = Avalonia.Controls.Primitives.ScrollBarVisibility.Auto
        };
        var layout = new DockPanel();
        DockPanel.SetDock(sidebar, Dock.Left);
        layout.Children.Add(sidebar);
        layout.Children.Add(scroll);
        Root.Children.Clear();
        Root.Children.Add(layout);
        UpdateGameInfo();
        _gameTimer.Start();
    }

    private async void CardClicked(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (_checking || sender is not Button button || button.Tag is not Card card || card.IsMatched || card.IsFaceUp)
        {
            return;
        }

        card.IsFaceUp = true;
        button.Content = card.Picture;
        button.Background = Brush.Parse("#FDE68A");
        button.IsHitTestVisible = false;

        if (_firstCard is null)
        {
            _firstCard = button;
            return;
        }

        _checking = true;
        _moves++;
        var first = (Card)_firstCard.Tag!;
        if (first.Value == card.Value)
        {
            first.IsMatched = true;
            card.IsMatched = true;
            _firstCard.Background = Brush.Parse("#86EFAC");
            button.Background = Brush.Parse("#86EFAC");
            _firstCard = null;
            _checking = false;
            _matchedPairs++;
            _scoreValue += 100;
            UpdateGameInfo();
            if (_matchedPairs == _cards.Count / 2)
            {
                _gameTimer.Stop();
                _status.Text = "Gratulujeme! Hra je hotová.";
                await Task.Delay(500);
                ShowWinScreen();
            }
            return;
        }

        _scoreValue = Math.Max(0, _scoreValue - 10);
        UpdateGameInfo();
        await Task.Delay(1200);
        first.IsFaceUp = false;
        card.IsFaceUp = false;
        _firstCard.Content = "×";
        button.Content = "×";
        _firstCard.Background = Brush.Parse("#60A5FA");
        button.Background = Brush.Parse("#60A5FA");
        _firstCard.IsHitTestVisible = true;
        button.IsHitTestVisible = true;
        _firstCard = null;
        _checking = false;
    }

    private void ShowWinScreen()
    {
        _content.Children.Clear();
        _content.HorizontalAlignment = HorizontalAlignment.Center;
        _content.VerticalAlignment = VerticalAlignment.Center;
        _content.Spacing = 16;
        _content.Children.Add(new TextBlock
        {
            Text = "Výborně!",
            FontSize = 42,
            FontWeight = FontWeight.Bold,
            HorizontalAlignment = HorizontalAlignment.Center,
            Foreground = Brush.Parse("#166534")
        });
        _content.Children.Add(new TextBlock
        {
            Text = $"Skóre: {_scoreValue}    Tahy: {_moves}\nČas: {FormatElapsed()}",
            FontSize = 20,
            TextAlignment = TextAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center
        });
        var again = CreateButton("Nová hra", 180, 52, 20);
        again.Click += (_, _) => ShowStartScreen();
        _content.Children.Add(again);
        Root.Children.Clear();
        Root.Children.Add(_content);
    }

    private void UpdateGameInfo()
    {
        _score.Text = $"Skóre: {_scoreValue}   Dvojice: {_matchedPairs}/{_cards.Count / 2}";
        _timer.Text = $"Čas: {FormatElapsed()}";
    }

    private string FormatElapsed()
    {
        var elapsed = DateTime.Now - _startedAt;
        return $"{(int)elapsed.TotalMinutes:00}:{elapsed.Seconds:00}";
    }

    private static Button CreateButton(string text, double width, double height, double fontSize)
    {
        return new Button
        {
            Content = text,
            Width = width,
            Height = height,
            FontSize = fontSize,
            HorizontalContentAlignment = HorizontalAlignment.Center,
            VerticalContentAlignment = VerticalAlignment.Center,
            Background = Brush.Parse("#60A5FA"),
            Foreground = Brushes.White,
            Margin = new Thickness(2)
        };
    }

    private sealed class Card
    {
        public Card(int value, string picture, int index)
        {
            Value = value;
            Picture = picture;
            Index = index;
        }

        public int Value { get; }
        public string Picture { get; }
        public int Index { get; }
        public bool IsFaceUp { get; set; }
        public bool IsMatched { get; set; }
        public Button? Button { get; set; }
    }
}