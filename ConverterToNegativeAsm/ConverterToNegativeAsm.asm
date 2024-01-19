;    Temat projektu: Image Converter to Negative
;    Kr�tki opis algorytmu: Aplikacja pobiera zdj�cie zapisane jako bitmapa, po czym piksel po pikselu przelicza warto�ci kolor�w
;    RGB na ich warto�ci w negatywie wg. wzoru: 
;    newColor = ((100 - degree) * oldColor + (255 - oldColor) * degree) / 100
;    gdzie: 
;          newColor: to warto�� negatywu
;          oldColor: kolor na wej�ciu
;          degree: stopie� negatywu
;    Data: 12.01.2024r
;    Semestr/Rok akademicki: sem. V, r.a. 2023/2024
;    Nazwisko autora: Krzysztof Adam, Adam Kuraczy�ski, Bart�omiej K�dro�
;    Aktualn� wersj� programu: 1.0
;    Historia zmian: https://github.com/bartlomi/ConverterToNegative

.code 

; Opis procedury:
;   Na ca�o�� programu w asemblerze sk�adaj� si� trzy procedury: wektorowego dodawania, odejmowania i mno�enie, konieczne aby krok po kroku wykona� opisany
;   powy�ej algorytm.

; Kolejno�� wykonywania procedur: pierwsze odejmowanie -> drugie odejmowanie -> pierwsze mno�enie -> drugie mno�enie -> dodawanie

; MyProc1 - Procedura wykonuj�ca operacj� wektorow� dodawania

; Parametry wej�ciowe:
;   - RCX (x1): Tablica typu byte
;   - RDX (x2): Tablica typu byte
;   - R8 (wynik): tablica wynikowa typu byte

; Parametry wyj�ciowe:
;   - R8: Tablica typu byte

.code
MyProc1 PROC
; color rcx -> xmm
; stala rdx -> xmm
; stop	r8 -> xmm

; wyjscie r8

movdqu	xmm0, [rcx]     ; Pierwsza tablica do xmm0
movdqu	xmm1, [rdx]     ; Druga tablica do xmm0

paddb		xmm0, xmm1      ; Dodawanie

movdqu	[r8], xmm0      ; Wynik do tablicy "zwracajacej"

ret
MyProc1 ENDP

; MyProc2 - Procedura wykonuj�ca operacj� wektorow� odejmowania

; Parametry wej�ciowe:
;   - RCX (x1): Tablica typu byte
;   - RDX (x2): Tablica typu byte
;   - R8 (wynik): tablica wynikowa typu byte

; Parametry wyj�ciowe:
;   - R8: Tablica typu byte

MyProc2 PROC
movdqu	xmm0, [rcx]     ; Pierwsza tablica do xmm0
movdqu	xmm1, [rdx]     ; Druga tablica do xmm0

psubb	xmm0, xmm1        ; Odejmowanie

movdqu	[r8], xmm0      ; Wynik do tablicy "zwracajacej"

ret
MyProc2 ENDP

; MyProc3 - Procedura wykonuj�ca operacj� wektorow� mno�enia

; Parametry wej�ciowe:
;   - RCX (x1): Tablica typu byte
;   - RDX (x2): Tablica typu byte
;   - R8 (wynik): tablica wynikowa typu byte

; Parametry wyj�ciowe:
;   - R8: Tablica typu byte

MyProc3 PROC

movdqu xmm0, [rcx]     ; Pierwsza tablica do xmm0
movdqu xmm1, [rdx]     ; Druga tablica do xmm1
pxor xmm2, xmm2        ; Zerowanie xmm2

; Mno�enie ni�szych bit�w
punpcklbw xmm0, xmm2   ; Ni�sze bity do xmm0
punpcklbw xmm1, xmm2   ; Ni�sze bity do xmm1
pmullw xmm0, xmm1      ; Mno�enie xmm0 i xmm1

; Mno�enie wy�szych bit�w
punpckhbw xmm3, xmm2   ; Wy�sze bity do xmm3
punpckhbw xmm4, xmm2   ; Wy�sze bity do xmm4
pmullw xmm3, xmm4      ; Mno�enie xmm3 i xmm4

; Combine the results
packuswb xmm0, xmm3    ; Wynik do xmm0
movdqu [r8], xmm0      ; Wynik do tablicy "zwracajacej"
    
ret
MyProc3 ENDP
end