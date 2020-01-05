# -*- mode: python ; coding: utf-8 -*-

block_cipher = None


a = Analysis(['pyDownloader.py'],
             pathex=['C:\\Users\\nouyo\\OneDrive\\Bureau\\Programmation\\Mobile-Multiplayer-Action-Game-in-Unity-400d9743b6143cfb84435abd2acaa1387e81b2bd\\VirusWar\\Assets\\Resources\\Songs'],
             binaries=[],
             datas=[],
             hiddenimports=[],
             hookspath=[],
             runtime_hooks=[],
             excludes=[],
             win_no_prefer_redirects=False,
             win_private_assemblies=False,
             cipher=block_cipher,
             noarchive=False)
pyz = PYZ(a.pure, a.zipped_data,
             cipher=block_cipher)
exe = EXE(pyz,
          a.scripts,
          a.binaries,
          a.zipfiles,
          a.datas,
          [],
          name='pyDownloader',
          debug=False,
          bootloader_ignore_signals=False,
          strip=False,
          upx=True,
          upx_exclude=[],
          runtime_tmpdir=None,
          console=True )
