;    Temat projektu: Image Converter to Negative
;    Kr�tki opis algorytmu: Aplikacja pobiera zdj�cie zapisane jako bitmapa, nast�pnie piksel po pikselu przelicza warto�ci kolor�w
;    RGB na ich warto�ci w negatywie wg. wzoru: 
;    newColor = ((100 - degree) * oldColor + (255 - oldColor) * degree) / 100
;    gdzie: 
;          newColor: to warto�� negatywu
;          oldColor: kolor na wej�ciu
;          degree: stopie� negatywu
;    Dat�: 12.01.2024r
;    Semestr/Rok akademicki: se. V, r.a. 2023/2024
;    Nazwisko autora: Krzysztof Adam, Adam Kuraczy�ski, Bart�omiej K�dro�
;    Aktualn� wersj� programu: 1.1
;    Historia zmian: https://github.com/bartlomi/ConverterToNegative

.code 

; MyProc1 - Procedura wykonuj�ca konwersj� koloru na negatyw z uwzgl�dnieniem stopnia negatywu

; Opis procedury:
;   MyProc1 jest procedur� odpowiedzialn� za konwersj� koloru piksela obrazu na jego negatyw,
;   przy uwzgl�dnieniu stopnia negatywu okre�lonego przez parametr wej�ciowy.

; Parametry wej�ciowe:
;   - RCX (x1): Warto�� koloru piksela (0-255), gdzie 0 to pe�ny kolor czarny, a 255 to pe�ny kolor bia�y.
;   - RDX (x2): Stopie� negatywu (0-100), gdzie 0 oznacza brak negatywu, a 100 to pe�ny negatyw.

; Parametry wyj�ciowe:
;   - RAX: Skonwertowana warto�� koloru piksela po zastosowaniu negatywu.

; Rejestry i znaczniki (flagi) ulegaj�ce zmianie:
;   - RAX: Zawiera skonwertowan� warto�� koloru piksela.
;   - R8: Tymczasowy rejestr wykorzystywany w obliczeniach.
;   - RDX: Zostaje wyzerowany przed dzieleniem.

MyProc1 proc 

; wartosc koloru (x1) 	RCX
; stopien negatywu (x2) RDX
; wyj�cie RAX

mov RAX, 100	;100 do RAX
sub RAX, RDX	;100 - x2
imul RAX, RCX	;(100 - x2) * x1

mov R8, 255		;255 do RBX
sub R8, RCX		;255 - x1
imul R8, RDX	;(255 - x1) * x2

add RAX, R8		;(100 - x2) * x1 + (255 - x1) * x2

sub RDX, RDX	;zerowanie RDX (konieczne przy dzieleniu)
mov RCX, 100	;100 do RCX

div RCX				;((100 - x2) * x1 + (255 - x1) * x2) / 100

ret 
MyProc1 endp 
end