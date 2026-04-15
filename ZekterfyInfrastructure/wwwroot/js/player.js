const MusicPlayer = {
    queue: [],
    currentIndex: -1,
    currentHowl: null,

    addAndPlay: function (songId, title) {
        const songObj = {
            id: songId,
            title: title,
            // ТУТ ВАЖЛИВО: це посилання на ваш метод у C#
            url: `/Songs/Stream?id=${songId}`
        };

        this.queue.push(songObj);

        if (this.currentHowl === null) {
            this.playNext();
        }
    },

    playNext: function () {
        if (this.currentIndex >= this.queue.length - 1) return;

        if (this.currentHowl) this.currentHowl.unload();

        this.currentIndex++;
        const nextSong = this.queue[this.currentIndex];

        document.getElementById("now-playing-title").innerText = nextSong.title;

        this.currentHowl = new Howl({
            src: [nextSong.url],
            html5: true,
            format: ['mp3'],
            onend: () => this.playNext()
        });

        this.currentHowl.play();
    },

    pause: function () { if (this.currentHowl) this.currentHowl.pause(); },
    resume: function () { if (this.currentHowl) this.currentHowl.play(); }
};