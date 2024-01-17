;    Temat projektu: Image Converter to Negative
;    Kr�tki opis algorytmu: Aplikacja pobiera zdj�cie zapisane jako bitmapa, po czym piksel po pikselu przelicza warto�ci kolor�w
;    RGB na ich warto�ci w negatywie wg. wzoru: 
;    newColor = ((100 - degree) * oldColor + (255 - oldColor) * degree) / 100
;    gdzie: 
;          newColor: to warto�� negatywu
;          oldColor: kolor na wej�ciu
;          degree: stopie� negatywu
;    Data: 12.01.2024r
;    Semestr/Rok akademicki: se. V, r.a. 2023/2024
;    Nazwisko autora: Krzysztof Adam, Adam Kuraczy�ski, Bart�omiej K�dro�
;    Aktualn� wersj� programu: 1.0
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

.code
MyProc1 PROC
; color rcx -> xmm
; stala rdx -> xmm
; stop	r8 -> xmm

; wyjscie r8

movdqu	xmm0, [rcx]
movdqu	xmm1, [rdx]

paddb		xmm0, xmm1

movdqu	[r8], xmm0

ret
MyProc1 ENDP

MyProc2 PROC
movdqu	xmm0, [rcx]
movdqu	xmm1, [rdx]

psubb	xmm0, xmm1

movdqu	[r8], xmm0

ret
MyProc2 ENDP

MyProc3 PROC

    movdqu xmm0, [rcx]     ; Za�aduj pierwsz� tablice do xmm0
    movdqu xmm1, [rdx]     ; Za�aduj drug� tablice do xmm1
    pxor xmm2, xmm2        ; Wyzeruj xmm2 (pomaga w mno�eniu)

    ; Mno�enie ni�szych bit�w
    punpcklbw xmm0, xmm2   ; Wypakuj ni�sze bity do xmm0
    punpcklbw xmm1, xmm2   ; Wypakuj ni�sze bity do xmm1
    pmullw xmm0, xmm1      ; Pomn� xmm0 i xmm1

    ; Mno�enie wy�szych bit�w
    punpckhbw xmm3, xmm2   ; Wypakuj wy�sze bity do xmm3
    punpckhbw xmm4, xmm2   ; Wypakuj ni�sze bity do xmm4
    pmullw xmm3, xmm4      ; Pomn� xmm3 i xmm4

    ; Combine the results
    packuswb xmm0, xmm3    ; Zapisz wynik do xmm0
    movdqu [r8], xmm0      ; Zapisz wynik w podanej tablicy
    
ret
MyProc3 ENDP
end