/*
Opis klas w projekcie i ich przeznaczenia : 

ENTITY
skrypt ktory powinien posiadac kazdy element w grze wystepujacy fizycznie na scenie - klikalny i interaktywny
kazdy taki entity ma swojego OWNERa, owner moze byc NULLEM

BUILDING:ENTITY
opisuje budynek

ANIMAL:ENTITY
opisuje wroga jednostke 

AVATAR:ENTITY
opisuje jendostke gracz W SCENIE

RESOURCEENTITYPARAMS
wykorzystywane jako kontener na parametry dla resource entity - typu entity ktory moze byc posiadany
czyli jednostki, buildingu etc,

MovingUnit
jednostka ktora moze zarzadzac gracz. moze sie poruszac.

PlayerAvatarController
obsluguje wszystkie inputy etc zwazane z OBECNYM AVATAREM GRACZA.

--------------

RegistryEditor
edytor dla Registry

CameraController
obiekt kontrolujacy kamere FIZYCZNIE podpiety do main camery.

CameraManager
zarzadza zachowaniem sie kamery - ustawienia.

----------------

GameService
Service for all the in-game logic. Manages all the rules of the game.

Player
to jest zbior METAINFORMACJI na temat gracza. 
playerType okresla czy jest to obecny gracz czy jakis inny gracz etc.

PlayerResourceType
typ zasobu z ktorego korzysta sie w grze,

PlayerSettingsBase
baza ustawien

Registry
klasa przechowujaca wszystkie globalne ustawienia.

SelectionType
typ zaznaczenia

UserInputService
wszystkei rzeczy zwiazane ze sterowaniem graczem!

-----------

GUIService

SelectionManager
fabryka do zaznaczan

InputService
zalatwia peryferia

HUDManager
zarzadza HUDem

----------------

EntityRepository - zawiera wszystkie obiekty i prefaby dostepne w grze


*/