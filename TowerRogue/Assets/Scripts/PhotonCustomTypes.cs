using ExitGames.Client.Photon;
using System.IO;

public static class PhotonCustomTypes
{
    public static void Register()
    {
        PhotonPeer.RegisterType(typeof(EnemyStats), (byte)'E',
            SerializeEnemyStats, DeserializeEnemyStats);
    }

    private static short SerializeEnemyStats(StreamBuffer outStream, object customObject)
    {
        EnemyStats stats = (EnemyStats)customObject;
        using (MemoryStream ms = new MemoryStream())
        using (BinaryWriter bw = new BinaryWriter(ms))
        {
            bw.Write(stats.enemyID);
            bw.Write(stats.enemyName ?? "");
            bw.Write(stats.maxHp);
            bw.Write(stats.atkPow);
            bw.Write(stats.atkRate);
            bw.Write(stats.shotRange);
            bw.Write(stats.moveSpeed);
            bw.Write((int)stats.targetType);
            bw.Write((int)stats.attackType);

            bw.Flush();
            byte[] data = ms.ToArray();
            outStream.Write(data, 0, data.Length);
            return (short)data.Length;
        }
    }

    private static object DeserializeEnemyStats(StreamBuffer inStream, short length)
    {
        byte[] data = new byte[length];
        inStream.Read(data, 0, length);
        using (MemoryStream ms = new MemoryStream(data))
        using (BinaryReader br = new BinaryReader(ms))
        {
            EnemyStats stats = new EnemyStats
            {
                enemyID = br.ReadInt32(),
                enemyName = br.ReadString(),
                maxHp = br.ReadInt32(),
                atkPow = br.ReadInt32(),
                atkRate = br.ReadInt32(),
                shotRange = br.ReadSingle(),
                moveSpeed = br.ReadInt32(),
                targetType = (EnemyStats.TargetType)br.ReadInt32(),
                attackType = (EnemyStats.AttackType)br.ReadInt32()
            };
            return stats;
        }
    }
}
