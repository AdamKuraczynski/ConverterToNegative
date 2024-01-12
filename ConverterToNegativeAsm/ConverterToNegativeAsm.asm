;    Temat projektu: Image Converter to Negative
;    Krótki opis algorytmu: Aplikacja pobiera zdjêcie zapisane jako bitmapa, nastêpnie piksel po pikselu przelicza wartoœci kolorów
;    RGB na ich wartoœci w negatywie wg. wzoru: 
;    newColor = ((100 - degree) * oldColor + (255 - oldColor) * degree) / 100
;    gdzie: 
;          newColor: to wartoœæ negatywu
;          oldColor: kolor na wejœciu
;          degree: stopieñ negatywu
;    Datê: 12.01.2024r
;    Semestr/Rok akademicki: se. V, r.a. 2023/2024
;    Nazwisko autora: Krzysztof Adam, Adam Kuraczyñski, Bart³omiej Kêdroñ
;    Aktualn¹ wersjê programu: 1.1
;    Historia zmian: https://github.com/bartlomi/ConverterToNegative

.code 

; MyProc1 - Procedura wykonuj¹ca konwersjê koloru na negatyw z uwzglêdnieniem stopnia negatywu

; Opis procedury:
;   MyProc1 jest procedur¹ odpowiedzialn¹ za konwersjê koloru piksela obrazu na jego negatyw,
;   przy uwzglêdnieniu stopnia negatywu okreœlonego przez parametr wejœciowy.

; Parametry wejœciowe:
;   - RCX (x1): Wartoœæ koloru piksela (0-255), gdzie 0 to pe³ny kolor czarny, a 255 to pe³ny kolor bia³y.
;   - RDX (x2): Stopieñ negatywu (0-100), gdzie 0 oznacza brak negatywu, a 100 to pe³ny negatyw.

; Parametry wyjœciowe:
;   - RAX: Skonwertowana wartoœæ koloru piksela po zastosowaniu negatywu.

; Rejestry i znaczniki (flagi) ulegaj¹ce zmianie:
;   - RAX: Zawiera skonwertowan¹ wartoœæ koloru piksela.
;   - R8: Tymczasowy rejestr wykorzystywany w obliczeniach.
;   - RDX: Zostaje wyzerowany przed dzieleniem.

MyProc1 proc 

; wartosc koloru (x1) 	RCX
; stopien negatywu (x2) RDX
; wyjœcie RAX

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