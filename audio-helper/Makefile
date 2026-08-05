# Helios audio helper build.
# Produces a single self-contained ELF `helios-audio` which is then embedded
# in the main helios AOT binary by build.sh.

CC      ?= cc
STRIP   ?= strip

CFLAGS  ?= -O2 -flto -fno-strict-aliasing -Wall -Wextra
CFLAGS  += -I. -DRNNOISE_BUILD

PW_CFLAGS = $(shell pkg-config --cflags libpipewire-0.3)
PW_LIBS   = $(shell pkg-config --libs libpipewire-0.3)

LDLIBS  = $(PW_LIBS) -lm -lpthread
LDFLAGS = -flto

RNN_SRCS = rnnoise/denoise.c rnnoise/rnn.c rnnoise/rnn_data.c \
           rnnoise/rnn_reader.c rnnoise/pitch.c rnnoise/kiss_fft.c \
           rnnoise/celt_lpc.c

RNN_OBJS = $(RNN_SRCS:.c=.o)

all: helios-audio

helios-audio: main.o $(RNN_OBJS)
	$(CC) $(LDFLAGS) -o $@ $^ $(LDLIBS)
	$(STRIP) $@

main.o: main.c protocol.h rnnoise/rnnoise.h
	$(CC) $(CFLAGS) $(PW_CFLAGS) -c main.c -o $@

rnnoise/%.o: rnnoise/%.c
	$(CC) $(CFLAGS) -c $< -o $@

clean:
	rm -f main.o $(RNN_OBJS) helios-audio

.PHONY: all clean
