using System.Windows;

namespace KooliProjekt.WpfApplication
{
    public class DialogProvider : IDialogProvider
    {
        public void ShowError(string message)
        {
            MessageBox.Show(message, "Viga", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public bool ConfirmDelete()
        {
            return MessageBox.Show(
                "Kas oled kindel, et soovid toote kustutada?",
                "Kinnita kustutamine",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes;
        }
    }
}
