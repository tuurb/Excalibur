Excalibur
=========
Een oefening in WPF, MVVM en ReactiveUI.


Must haves
----------
- Gebruik de reeds aanwezige LoginManager om in te loggen:
  - inloggen is succesvol wanneer Username en Password gelijk zijn,
  - er wordt een exception gegenereerd wanneer de Username 'error' is.
- Toon Status tekst bij succesvolle login.
- Toon Status tekst bij ongeldige Username/Password combinatie.
- Toon Status tekst met exception message wanneer er iets fout gaat tijdens het inloggen.
- LoginButton is alleen enabled wanneer zowel Username als Password niet leeg zijn.
- De lijst met wachttijden (in seconde) bevat de waarden 0..9 en de waarde 2 is initieel geselecteerd.

Nice to haves
-------------
- Toon Status tekst wanneer het inloggen is onderbroken.
- Leeg Status wanneer de LoginButton disabled is.
- Leeg Status tijdens het inloggen.
- LoginButton is disabled wanneer de applicatie bezig is met inloggen, daarna is deze weer enabled. 
- CancelButton is alleen enabled wanneer de applicatie bezig is met inloggen, daarna is deze weer disabled.
- De applicatie window kan verplaatst worden terwijl het inloggen bezig is.

Stap 1
------
Probeer eerst zoveel mogelijk van bovenstaande specificaties te implementeren in de codebehind van de LoginView zonder een apart ViewModel te gebruiken (dit lijkt het meest op hoe er de afgelopen jaren in onze applicatie is gewerkt).

Stap 2 (optioneel)
------------------
Een (tussen-)stap in de goede richting is het gebruik van een apart ViewModel om de logica los te krijgen van de view. 

Verander in MainWindow.xaml regel 14 <stap1:LoginView /> naar <stap2:LoginView />.

Verplaats eerst de properties van de LoginView naar het LoginViewModel. 

Zet in de constructor van de View de property DataContext op een nieuwe instantie van LoginViewModel.  

De View hoeft daarna niet meer van INotifyPropertyChanged af te leiden en het PropertyChanged event en de OnPropertyChanged methode kunnen ook weg.

Probeer nu zoveel mogelijk van bovenstaande specificaties te implementeren in het LoginViewModel.

N.B. Er staat de folder van deze stap een RelayCommand die je hier zou kunnen gebruiken (zie https://msdn.microsoft.com/en-us/magazine/dd419663.aspx#id0090030).

Stap 3
------
We gaan nu ReactiveUI gebruiken.
 
Verander in MainWindow.xaml regel 14 <stap2:LoginView /> naar <stap3:LoginView />.

Verplaats eerst de properties van de LoginView naar het LoginViewModel. 
Gebruik RaiseAndSetIfChanged in de setters van de properties. Initialiseer de lijst met wachttijden en de initiele selectie in de constructor.

De View moet afgeleid zijn van de interface IViewFor< LoginViewModel>. Implementeer de missende members en zet in de constructor de property ViewModel  op een nieuwe instantie van LoginViewModel.

De View hoeft daarna niet meer van INotifyPropertyChanged af te leiden en het PropertyChanged event en de OnPropertyChanged methode kunnen ook weg.

In de XAML kunnen de bindings van de controls worden verwijderd, dit gebeurt namelijk in de codebehind (uitzondering is de Binding in de datatemplate van de combobox, die moet blijven staan). 

Gebruik WhenActivated, BindTo, OneWayBind, BindToCommand, etc. in de constructor van de view om ViewModel en View aan elkaar te koppelen.
In het Excalibur.Tests project zijn wat unit tests opgenomen die te gebruiken constructies uitleggen. 

