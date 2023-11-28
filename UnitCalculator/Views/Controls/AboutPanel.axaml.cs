using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace UnitCalculator.Views.Controls;

public partial class AboutPanel : Window
{
    public static AboutPanel? ActivePanel { get; set; }
    
    public AboutPanel()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    
    public enum MessageBoxResult
    {
        OK
    }
    
    public new static Task<MessageBoxResult> Show(Window? p_parent)
    {
        ActivePanel = new AboutPanel();

        if (p_parent != null)
        {
            ActivePanel.ShowDialog(p_parent);
        }
        else
        {
            ActivePanel.Show();
        }
        
        return Task.FromResult(MessageBoxResult.OK);
    }

    private void Button_OnClick(object? p_sender, RoutedEventArgs p_e)
    {
        ActivePanel?.Close();
    }
}