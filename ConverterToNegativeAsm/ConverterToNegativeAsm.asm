;    Temat projektu: Image Converter to Negative
;    Krótki opis algorytmu: Aplikacja pobiera zdjêcie zapisane jako bitmapa, po czym piksel po pikselu przelicza wartoœci kolorów
;    RGB na ich wartoœci w negatywie wg. wzoru: 
;    newColor = ((100 - degree) * oldColor + (255 - oldColor) * degree) / 100
;    gdzie: 
;          newColor: to wartoœæ negatywu
;          oldColor: kolor na wejœciu
;          degree: stopieñ negatywu
;    Data: 12.01.2024r
;    Semestr/Rok akademicki: se. V, r.a. 2023/2024
;    Nazwisko autora: Krzysztof Adam, Adam Kuraczyñski, Bart³omiej Kêdroñ
;    Aktualn¹ wersjê programu: 1.0
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

    movdqu xmm0, [rcx]     ; Za³aduj pierwsz¹ tablice do xmm0
    movdqu xmm1, [rdx]     ; Za³aduj drug¹ tablice do xmm1
    pxor xmm2, xmm2        ; Wyzeruj xmm2 (pomaga w mno¿eniu)

    ; Mno¿enie ni¿szych bitów
    punpcklbw xmm0, xmm2   ; Wypakuj ni¿sze bity do xmm0
    punpcklbw xmm1, xmm2   ; Wypakuj ni¿sze bity do xmm1
    pmullw xmm0, xmm1      ; Pomnó¿ xmm0 i xmm1

    ; Mno¿enie wy¿szych bitów
    punpckhbw xmm3, xmm2   ; Wypakuj wy¿sze bity do xmm3
    punpckhbw xmm4, xmm2   ; Wypakuj ni¿sze bity do xmm4
    pmullw xmm3, xmm4      ; Pomnó¿ xmm3 i xmm4

    ; Combine the results
    packuswb xmm0, xmm3    ; Zapisz wynik do xmm0
    movdqu [r8], xmm0      ; Zapisz wynik w podanej tablicy
    
ret
MyProc3 ENDP
end