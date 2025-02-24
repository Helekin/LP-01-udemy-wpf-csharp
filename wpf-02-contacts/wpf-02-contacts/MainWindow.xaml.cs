﻿using System.Windows;
using System.Windows.Controls;
using wpf_02_contacts.Classes;

namespace wpf_02_contacts;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    List<Contact> contacts;
    public MainWindow()
    {
        InitializeComponent();

        contacts = new List<Contact>();

        ReadDatabase();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        NewContactWindow newContactWindow = new NewContactWindow();
        newContactWindow.ShowDialog();

        ReadDatabase();
    }

    void ReadDatabase()
    {
        using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.databasePath))
        {
            conn.CreateTable<Contact>();
            contacts = (conn.Table<Contact>().ToList()).OrderBy(contact => contact.Name).ToList();
        }

        if (contacts != null)
        {
            contactsListView.ItemsSource = contacts;
        }
    }

    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        TextBox searchTextBox = sender as TextBox;

        var filteredList = contacts.Where(contact => contact.Name.ToLower().Contains(searchTextBox.Text.ToLower())).ToList();

        contactsListView.ItemsSource = filteredList;
    }
}