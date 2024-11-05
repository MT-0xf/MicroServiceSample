package interfaces;

import generateImageService.GenerateImageServiceOuterClass;
import mainBusiness.MainBusinessOuterClass;
import java.util.List;

/**
 * ダウンロードサービスインターフェース
 */
public interface IDownloadService {
    /**
     * 画像一覧取得
     * @return 画像一覧
     * @throws InterruptedException
     */
    public List<MainBusinessOuterClass.PostImageFile> getPostImageList() throws InterruptedException;

    /**
     * 画像生成ファイル取得
     * @return 生成画像
     */
    public GenerateImageServiceOuterClass.ImageFile generateImage();
}
