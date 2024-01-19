;    Temat projektu: Image Converter to Negative
;    Krótki opis algorytmu: Aplikacja pobiera zdjêcie zapisane jako bitmapa, po czym piksel po pikselu przelicza wartoœci kolorów
;    RGB na ich wartoœci w negatywie wg. wzoru: 
;    newColor = ((100 - degree) * oldColor + (255 - oldColor) * degree) / 100
;    gdzie: 
;          newColor: to wartoœæ negatywu
;          oldColor: kolor na wejœciu
;          degree: stopieñ negatywu
;    Data: 12.01.2024r
;    Semestr/Rok akademicki: sem. V, r.a. 2023/2024
;    Nazwisko autora: Krzysztof Adam, Adam Kuraczyñski, Bart³omiej Kêdroñ
;    Aktualn¹ wersjê programu: 1.0
;    Historia zmian: https://github.com/bartlomi/ConverterToNegative

.code 

; Opis procedury:
;   Na ca³oœæ programu w asemblerze sk³adaj¹ siê trzy procedury: wektorowego dodawania, odejmowania i mno¿enie, konieczne aby krok po kroku wykonaæ opisany
;   powy¿ej algorytm.

; Kolejnoœæ wykonywania procedur: pierwsze odejmowanie -> drugie odejmowanie -> pierwsze mno¿enie -> drugie mno¿enie -> dodawanie

; MyProc1 - Procedura wykonuj¹ca operacjê wektorow¹ dodawania

; Parametry wejœciowe:
;   - RCX (x1): Tablica typu byte
;   - RDX (x2): Tablica typu byte
;   - R8 (wynik): tablica wynikowa typu byte

; Parametry wyjœciowe:
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

; MyProc2 - Procedura wykonuj¹ca operacjê wektorow¹ odejmowania

; Parametry wejœciowe:
;   - RCX (x1): Tablica typu byte
;   - RDX (x2): Tablica typu byte
;   - R8 (wynik): tablica wynikowa typu byte

; Parametry wyjœciowe:
;   - R8: Tablica typu byte

MyProc2 PROC
movdqu	xmm0, [rcx]     ; Pierwsza tablica do xmm0
movdqu	xmm1, [rdx]     ; Druga tablica do xmm0

psubb	xmm0, xmm1        ; Odejmowanie

movdqu	[r8], xmm0      ; Wynik do tablicy "zwracajacej"

ret
MyProc2 ENDP

; MyProc3 - Procedura wykonuj¹ca operacjê wektorow¹ mno¿enia

; Parametry wejœciowe:
;   - RCX (x1): Tablica typu byte
;   - RDX (x2): Tablica typu byte
;   - R8 (wynik): tablica wynikowa typu byte

; Parametry wyjœciowe:
;   - R8: Tablica typu byte

MyProc3 PROC

movdqu xmm0, [rcx]     ; Pierwsza tablica do xmm0
movdqu xmm1, [rdx]     ; Druga tablica do xmm1
pxor xmm2, xmm2        ; Zerowanie xmm2

; Mno¿enie ni¿szych bitów
punpcklbw xmm0, xmm2   ; Ni¿sze bity do xmm0
punpcklbw xmm1, xmm2   ; Ni¿sze bity do xmm1
pmullw xmm0, xmm1      ; Mno¿enie xmm0 i xmm1

; Mno¿enie wy¿szych bitów
punpckhbw xmm3, xmm2   ; Wy¿sze bity do xmm3
punpckhbw xmm4, xmm2   ; Wy¿sze bity do xmm4
pmullw xmm3, xmm4      ; Mno¿enie xmm3 i xmm4

; Combine the results
packuswb xmm0, xmm3    ; Wynik do xmm0
movdqu [r8], xmm0      ; Wynik do tablicy "zwracajacej"
    
ret
MyProc3 ENDP
end