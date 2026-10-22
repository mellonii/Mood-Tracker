-- -----------------------------------------------------------------------------
-- 1. Тип настроения
-- -----------------------------------------------------------------------------
CREATE TYPE mood_level AS ENUM (
    'awful',     -- ужасно
    'bad',       -- плохо
    'neutral',   -- норм
    'good',      -- хорошо
    'great'      -- отлично
);


-- -----------------------------------------------------------------------------
-- 2. Пользователи
-- -----------------------------------------------------------------------------
CREATE TABLE users (
                       id            BIGSERIAL    PRIMARY KEY,
                       username      VARCHAR(50)  NOT NULL UNIQUE,
                       password_hash VARCHAR(255) NOT NULL,
                       created_at    TIMESTAMPTZ  NOT NULL DEFAULT NOW()
    -- Future: pin_code_hash VARCHAR(255), hidden_mode_enabled BOOLEAN DEFAULT FALSE
);

COMMENT ON TABLE  users IS 'Пользователи приложения';


-- -----------------------------------------------------------------------------
-- 3. Дни
-- -----------------------------------------------------------------------------
CREATE TABLE days (
                      id         BIGSERIAL   PRIMARY KEY,
                      user_id    BIGINT      NOT NULL REFERENCES users(id) ON DELETE CASCADE,
                      day_date   DATE        NOT NULL,
                      created_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    -- Future: sleep_hours NUMERIC(4,2), period BOOLEAN, day_note TEXT
                      CONSTRAINT uq_days_user_date UNIQUE (user_id, day_date)
);

CREATE INDEX idx_days_user_date_desc ON days(user_id, day_date DESC);

COMMENT ON TABLE days IS
    'День пользователя. Один на пару (user, date). Сюда вешаются дневные атрибуты: сон, месячные, заметка к дню.';


-- -----------------------------------------------------------------------------
-- 4. Причины настроения (справочник)
-- -----------------------------------------------------------------------------
CREATE TABLE reasons (
                         id    SMALLSERIAL PRIMARY KEY,
                         code  VARCHAR(20) NOT NULL UNIQUE,   -- стабильный код для приложения
                         label VARCHAR(50) NOT NULL           -- человекочитаемое название
);

INSERT INTO reasons (code, label) VALUES
                                      ('sleep',   'Сон'),
                                      ('work',    'Работа'),
                                      ('sport',   'Спорт'),
                                      ('friends', 'Друзья');

-- -----------------------------------------------------------------------------
-- 5. Записи о настроении
-- -----------------------------------------------------------------------------
CREATE TABLE mood_entries (
                              id          BIGSERIAL   PRIMARY KEY,
                              user_id     BIGINT      NOT NULL REFERENCES users(id) ON DELETE CASCADE,
                              day_id      BIGINT      NOT NULL REFERENCES days(id)  ON DELETE CASCADE,

    -- Пользовательские поля
                              entry_date  DATE        NOT NULL,            -- за какой день запись (может быть вчера)
                              entry_time  TIME        NOT NULL,            -- в какой момент отметили
                              mood        mood_level  NOT NULL,
                              comment     TEXT,                            -- необязательный

    -- Технические поля
                              created_at  TIMESTAMPTZ NOT NULL DEFAULT NOW(),
                              updated_at  TIMESTAMPTZ NOT NULL DEFAULT NOW()

    -- Future: is_favorite BOOLEAN NOT NULL DEFAULT FALSE, photo_url TEXT
);

-- Главный индекс для экрана "история": новые сверху
CREATE INDEX idx_mood_entries_user_date_time
    ON mood_entries(user_id, entry_date DESC, entry_time DESC);

-- Индекс для связи с днём (для dashboard "все записи за день")
CREATE INDEX idx_mood_entries_day_id ON mood_entries(day_id);

-- -----------------------------------------------------------------------------
-- 6. Связь настроений с причинами (many-to-many)
-- -----------------------------------------------------------------------------
CREATE TABLE mood_entry_reasons (
                                    mood_entry_id BIGINT   NOT NULL REFERENCES mood_entries(id) ON DELETE CASCADE,
                                    reason_id     SMALLINT NOT NULL REFERENCES reasons(id)      ON DELETE CASCADE,
                                    PRIMARY KEY (mood_entry_id, reason_id)
);

-- Обратный индекс: "найти все записи, где причиной был спорт"
CREATE INDEX idx_mood_entry_reasons_reason_id ON mood_entry_reasons(reason_id);

-- -----------------------------------------------------------------------------
-- 7. Триггер: авто-обновление updated_at при редактировании
-- -----------------------------------------------------------------------------
CREATE OR REPLACE FUNCTION set_updated_at()
RETURNS TRIGGER AS $$
BEGIN
    NEW.updated_at = NOW();
RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_mood_entries_updated_at
    BEFORE UPDATE ON mood_entries
    FOR EACH ROW
    EXECUTE FUNCTION set_updated_at();