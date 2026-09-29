# Survivors (estilo Vampire Survivors) — Unity 6

Todo o jogo é montado em runtime por `GameBootstrap`, então basta uma cena vazia.

## Controles
WASD / setas para mover. A arma dispara sozinha no inimigo mais próximo. Colete gemas verdes para subir de nível e escolher upgrades. `R` reinicia após o Game Over.

## Unity CLI
Instale o Unity 6 (6000.0.x) pelo Unity Hub e ative a licença. Depois:

```bash
# Hub CLI (opcional): instalar o editor
"Unity Hub" -- --headless install --version 6000.0.23f1

# Build pela linha de comando (cria Assets/Scenes/Main.unity se não existir)
Unity -batchmode -quit -projectPath . -executeMethod BuildScript.BuildWindows -logFile -
# Também existem: BuildScript.BuildLinux, BuildScript.BuildMac, BuildScript.BuildWebGL
```

Ou abra a pasta no Unity Hub e aperte Play em qualquer cena.
